using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movies.Data;
using Movies.DTOs.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Movies.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IConfiguration configuration;
		private readonly UserManager<AppUser> userManager;
		private readonly IMapper mapper;
		private readonly AppDbContext context;

		public AccountController( IConfiguration configuration,UserManager<AppUser> userManager ,IMapper mapper,AppDbContext context)
		{
			this.configuration = configuration;
			this.userManager = userManager;
			this.mapper = mapper;
			this.context = context;
		}


		[HttpPost("Register")]

		public async Task<IActionResult> Register(Register Model)
		{
			

			if(!ModelState.IsValid) 
				return BadRequest(ModelState);

			var MappedData=mapper.Map<AppUser>(Model);
			MappedData.UserName = Model.Email;
			var result= await userManager.CreateAsync(MappedData,Model.Password);

			if(!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(error.Code, error.Description);
				}
				return BadRequest(ModelState);
			}


			var ToAddUser =
				await context.Users.FirstOrDefaultAsync(d => d.Email == MappedData.Email);

			var Users =
				await context.Users.ToListAsync();

			var RoleName =
				(Users.Count() > 1) ? "User" : "Admin";
			 var RoleSaveResult=	await userManager.AddToRoleAsync(ToAddUser, RoleName);

			if (!RoleSaveResult.Succeeded)
			{
				//todo removeUserby Email in Account service

				var ToDEleteUSer =
					await context.Users.FirstOrDefaultAsync(d => d.Email == MappedData.Email);
				var ReomveUser =
					    context.Users.Remove(ToDEleteUSer);
					 await	 context.SaveChangesAsync();

				///

				foreach(var err in RoleSaveResult.Errors)
				{

					ModelState.AddModelError(err.Code, err.Description);

				}
				return StatusCode(500, ModelState);

			}
			return Ok("User Registered Successfully");



		}



		[HttpPost("Login")]

		
		public async Task<IActionResult> Login(Login  model)
		{

			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			var EmailCheck=await 
				userManager.FindByEmailAsync(model.Email);
			if(EmailCheck==null)
				return BadRequest("Invalid Email or Password");

			var PasswordCheck= await
				userManager.CheckPasswordAsync(EmailCheck,model.Password);
			if(!PasswordCheck)
				return BadRequest("Invalid Email or Password");

			//design token

			///credentials

			////symmetric security key
			///
			var claims =
				new List<Claim>
				{
					new Claim(JwtRegisteredClaimNames.Sub,EmailCheck.Email),
					new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
					new Claim(ClaimTypes.NameIdentifier,EmailCheck.Id),
					new Claim(ClaimTypes.Email,EmailCheck.Email),
					new Claim(ClaimTypes.Name,EmailCheck.UserName),
					new Claim(ClaimTypes.Role,(await userManager.GetRolesAsync(EmailCheck)).FirstOrDefault() ?? "")
				};

			var secretKey=
				new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
					configuration["Jwt:Key"]
					));


			var credentials =
				new SigningCredentials(secretKey,SecurityAlgorithms.HmacSha256.ToString());
				var Tokendesigner =new JwtSecurityToken(
			 
					 issuer:configuration["Jwt:Issuer"],
					 audience:configuration["Jwt:Audience"],
					 expires:DateTime.Now.AddMinutes(60),
					 signingCredentials: credentials,
					 claims: claims

				 );


			//generate token

			var token= new JwtSecurityTokenHandler().WriteToken(Tokendesigner);

			return Ok(new {Token=token});




		}
	}
}
