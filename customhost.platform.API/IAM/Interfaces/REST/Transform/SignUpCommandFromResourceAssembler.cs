using customhost_backend.IAM.Domain.Model.Commands;
using customhost_backend.IAM.Interfaces.REST.Resources;

namespace customhost_backend.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password, resource.Role);
    }
}