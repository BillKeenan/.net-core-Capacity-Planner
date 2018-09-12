using System;
using System.Collections.Generic;

namespace bigmojo.net.capacity.api.Model {

    public class Assignment {

        public string Id  = "";

        private Dictionary<string,int[]> _assignments = new Dictionary<string,int[]>();

        public Assignment (Person person, int year) {
            Person = person;
            Year = year;
        }

        public Person Person { get; set; }
        public int Year { get; set; }
        public Dictionary<string, int[]> Assignments { get => _assignments; }

        internal void addAssignment (Project project, int week, int percentage) {

            if (! Assignments.ContainsKey(project.Id)){
                Assignments[project.Id] = new int[52];
            }

            Assignments[project.Id][week]=percentage;

        }
    }
}