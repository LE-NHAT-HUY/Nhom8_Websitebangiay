
@model Nhom8_WebSiteBanGiay.Models.TbAccount

@{
	ViewData["Title"] = "Login";
}

<h4 class="text-center">Đăng Nhập</h4>

<div class="row justify-content-md-center">
	<div class="col-md-4">
		<div class="card">
			<div class="card-header">
				Login
			</div>
			<div class="card-body">
				@using (Html.BeginForm("Login", "Home", FormMethod.Post))
				{
					@Html.AntiForgeryToken()
					<div class="form-group">
						@Html.LabelFor(m => m.Email)
						@Html.TextBoxFor(m => m.Email, "", new { @class = "form-control", @placeholder = "Email" })

					</div>
					<div class="form-group">
						@Html.LabelFor(m => m.Password)
						@Html.PasswordFor(m => m.Password, new { @class = "form-control", @placeholder = "Password" })
					</div>
					<div class="form-group">
						<input type="submit" name="submit" class="btn btn-primary" value="Login" />
					</div>

					<a href="@Url.Action("Register", "Home")" class="social-info">
						<p class="hover-text"> Đăng kí</p>
					</a>
				}
				</div>
			</div>
		</div>
	</div>
</div>

