namespace Lab13.Domain.Validators;

public interface IValidator<E>
{
    public void Validate(E input);
}