
namespace TC_Clinica_Gerenciamento.Models.Local
{
    public class ResultService<T>
        where T : new()
    {
        private bool _status;
        public string message { get; set; }
        public string errorMessage { get; set; }
        public bool status
        {
            get
            {
                _status = string.IsNullOrWhiteSpace(errorMessage);
                return _status;
            }
            set
            {
                _status = value;
            }
        }
        public T value { get; set; }
    }
}