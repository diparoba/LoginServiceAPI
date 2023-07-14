namespace API_PVIAcademico.DTO
{
    public class AuthResponse
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
