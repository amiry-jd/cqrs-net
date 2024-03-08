using System.Threading.Tasks;

namespace CQRS.NET {
    public interface IValidator<in TValidatable> {

        Task ValidateAsync(TValidatable validatable);

    }
}