using customhost_backend.IAM.Domain.Model.Aggregates;
using customhost_backend.IAM.Interfaces.REST.Resources;

namespace customhost_backend.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Username);
    }
}