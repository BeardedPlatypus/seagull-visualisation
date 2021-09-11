using System;
using System.Collections.Generic;
using System.Linq;
using BeardedPlatypus.Functional.NetCDF;

namespace Seagull.Visualisation.Tests.Components.Model
{
    /// <summary>
    /// The <see cref="TimeQuery"/> implements the <see cref="IQuery"/> to obtain
    /// the Time component of a NetCDF UGRID file.
    /// It retrieves the start time and time steps.
    /// </summary>
    public class TimeQuery : IQuery
    {
        public DateTime StartTime { get; private set; }

        public IList<TimeSpan> TimeSteps { get; private set; }

        public void Execute(IRepository repository)
        {
            // Retrieve the id by finding the first component that defines a time component.
            // In the case of UGRID files, this is a variable that has a standard_name attribute
            // set to "time"
            var ids = repository.RetrieveVariablesWithAttributeWithValue("standard_name", "time")
                .ToList();
            var id = ids[0];
            
            // The time variable should always have a "units" attribute, that defines a string 
            // as "<time-quantity> since <date>", for example "seconds since 2001-01-01 00:00:00 +00:00"
            var units = repository.RetrieveVariableAttribute<string>(id, "units");
            InterpretUnitsString(units.Values.First(), out var timeStep, out var startTime);
            StartTime = startTime;

            // The "time" variable is a 1D sequence of doubles, we will convert this to time spans
            // by using the <time-quantity> obtained from the units string.
            TimeSteps = repository.RetrieveVariableValue<double>(id)
                .Values
                .Select(GetToTimeStep(timeStep))
                .ToList();
        }

        private static void InterpretUnitsString(string units,
            out string o,
            out DateTime startTime)
        {
            string[] parts = units.Split(new[] { " since " }, StringSplitOptions.RemoveEmptyEntries);

            // We assume the string is correctly formatted. In production code we might want to add
            // some more validation here.
            o = parts[0];
            startTime = DateTime.Parse(parts[1]);
        }

        private static Func<double, TimeSpan> GetToTimeStep(string timeStepSize) =>
            timeStepSize switch
            {
                "seconds" => TimeSpan.FromSeconds,
                "hours" => TimeSpan.FromHours,
                "days" => TimeSpan.FromDays,
                _ => throw new ArgumentOutOfRangeException(nameof(timeStepSize), timeStepSize, null)
            };
    }
}