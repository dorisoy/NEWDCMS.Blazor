using DCMS.Application.Specifications.Base;
using DCMS.Domain.Misc;

namespace DCMS.Application.Specifications.Misc
{
    public class DocumentTypeFilterSpecification : SpecificationBase<DocumentType>
    {
        public DocumentTypeFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Description.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}