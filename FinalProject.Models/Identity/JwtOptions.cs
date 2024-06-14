namespace FinalProject.Models.Identity
{
    public class JwtOptions // sxva gzac mojna
    {
        // secret
        //issuer
        //Audience

        public string Secret { get; set; } = string.Empty;
        public string Issuer {  get; set; } = string.Empty;
        public string Audience {  get; set; } = string.Empty;
    }
}
