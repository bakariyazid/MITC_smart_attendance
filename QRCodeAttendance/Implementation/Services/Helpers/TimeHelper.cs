namespace QRCodeAttendance.Implementation.Services.Helpers
{
  
        public static class TimeHelper
        {
            private static readonly TimeZoneInfo AppZone = ResolveZone();

            private static TimeZoneInfo ResolveZone()
            {
                foreach (var id in new[] { "Africa/Lagos", "W. Central Africa Standard Time" })
                {
                    try { return TimeZoneInfo.FindSystemTimeZoneById(id); }
                    catch { }
                }
                return TimeZoneInfo.CreateCustomTimeZone("WAT", TimeSpan.FromHours(1), "WAT", "WAT");
            }

            // What the user typed (West Africa Time) -> UTC for the database
            public static DateTime ToUtcFromApp(DateTime value)
            {
                if (value == DateTime.MinValue) return DateTime.SpecifyKind(value, DateTimeKind.Utc);
                if (value.Kind == DateTimeKind.Utc) return value;
                return TimeZoneInfo.ConvertTimeToUtc(
                    DateTime.SpecifyKind(value, DateTimeKind.Unspecified), AppZone);
            }

            // UTC from the database -> West Africa Time for display
            public static DateTime ToAppLocal(DateTime utc)
            {
                var u = utc.Kind == DateTimeKind.Utc
                    ? utc
                    : DateTime.SpecifyKind(utc, DateTimeKind.Utc);
                return TimeZoneInfo.ConvertTimeFromUtc(u, AppZone);
            }
        }
    }           

