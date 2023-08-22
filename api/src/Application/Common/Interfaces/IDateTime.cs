namespace SiMinor7.Application.Common.Interfaces;

public interface IDateTime : ITransientService
{
    DateTime Now { get; }
}