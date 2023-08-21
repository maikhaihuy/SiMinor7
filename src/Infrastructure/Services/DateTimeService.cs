using SiMinor7.Application.Common.Interfaces;

namespace SiMinor7.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}