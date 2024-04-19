using BuildingManagerDomain.Entities;
using BuildingManagerModels.CustomExceptions;

namespace BuildingManagerModels.Inner
{
    public class CreateAdminRequest : CreateUserRequest<Admin>
    {
        public string Lastname { get; set; }

        public override Admin ToEntity()
        {
            var entity = base.ToEntity();
            entity.Lastname = this.Lastname;
            return entity;
        }

        public override void Validate()
        {
            base.Validate();
            if (string.IsNullOrEmpty(Lastname))
            {
                throw new InvalidArgumentException("lastname");
            }
        }
    }
}
