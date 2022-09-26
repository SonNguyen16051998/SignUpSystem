namespace SignupSystem.Models.ViewModel
{
    public class ViewToken<T>
    {
        public string Token { get; set; }
        public T User { get; set; }
    }
}
