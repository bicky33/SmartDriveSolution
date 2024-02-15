using Domain.Enum.Master;

namespace Contract.DTO.Master
{
    public class TemplateTypeResponse
    {

        public int TetyId { get; set; }
        public TemplateTypeNameEnum TetyName { get; set; }
        public TemplateTypeGroupEnum TetyGroup { get; set; }
    }
}