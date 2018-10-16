
namespace TCC_Unip.Models.Local
{
    public class ResultService<T>
        where T : new()
    {
        public string Message { get; set; }
        public bool Status { get; set; } = true;
        public T Value { get; set; }
    }
}