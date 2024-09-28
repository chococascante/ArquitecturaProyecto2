using DTO;

namespace DataAccess.Mappers
{
    public interface IObjectMapper
    {
        public BaseClass BuildObject(Dictionary<string, object> objectRow);
        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> objectRows);
    }
}
