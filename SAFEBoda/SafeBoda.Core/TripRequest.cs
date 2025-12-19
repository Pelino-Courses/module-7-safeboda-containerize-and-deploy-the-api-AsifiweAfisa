using System;
using SafeBoda.Core.Models;

namespace SafeBoda.Core
{
    public record TripRequest(Guid RecordId, Location Start, Location End, Decimal Fare);
}