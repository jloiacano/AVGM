using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AVGM.Models;   

namespace AVGM.DAL
{
    public class SchoolInitializer : System.Data.Entity.CreateDatabaseIfNotExists<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student
                {
                    StudentID = Guid.Parse("0132C0C3-CDC4-41F7-89E7-AA6C1452B58F"),
                    SSNumber = "111-11-1111",
                    FName = "ExampleFirstName1",
                    MName = "ExampleMidName1",
                    LName = "ExampleLastName1",
                    Gender = 'F',
                    DOB = DateTime.Parse("2012-09-01")
                }
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var guardian = new List<Guardian>
            {
                new Guardian
                {
                    GuardianID = Guid.Parse("E4F85065-AD5A-4122-8722-2CA89B514700"),
                    FName = "EX:Guardian FName1",
                    MName = "EX:Guardian MName1",
                    LName = "EX:Guardian LName1",
                    Gender = 'M',
                    DOB = DateTime.Parse("05/10/1975"),
                    Relationship = "Father",
                    PhoneNumber = "847-555-1111",
                    Email = "exampleGuardian1@example.com",
                    LivesWithStudent = true
                },

                new Guardian
                {
                    GuardianID = Guid.Parse("8519E1C1-466B-452D-9501-04CF5C9C1C09"),
                    FName = "EX:Guardian FName2",
                    MName = "EX:Guardian MName2",
                    LName = "EX:Guardian LName2",
                    Gender = 'M',
                    DOB = DateTime.Parse("09/22/1982"),
                    Relationship = "Mother",
                    PhoneNumber = "847-555-2222",
                    Email = "exampleGuardian2@example.com",
                    LivesWithStudent = true
                }
            };

            guardian.ForEach(s => context.Guardians.Add(s));
            context.SaveChanges();

            var addresses = new List<Address>
            {
            new Address{AddressID=Guid.Parse("B2BC45C2-CBDE-4036-B05A-0F9BE14A820C"), StreetAddress="EX:1234 Main St",City="EX:Libertyville", State="EX:IL", Zip="EX:60048"},
            new Address{AddressID=Guid.Parse("BA9ECE4B-E25C-41CA-BB60-6D09C477CE29"), StreetAddress="EX:2345 Dawes St",City="EX:Libertyville", State="EX:IL", Zip="EX:60048"},
            new Address{AddressID=Guid.Parse("D24EC251-E1F8-4F94-BA4C-A8236EB90234"), StreetAddress="EX:3456 Sunnyside Ct",City="EX:Libertyville", State="EX:IL", Zip="EX:60048"},
            new Address{AddressID=Guid.Parse("25630C37-8C24-4943-A3C7-3DF6DC723571"), StreetAddress="EX:456 Church St",City="EX:Libertyville", State="EX:IL", Zip="EX:60048"}
            };

            addresses.ForEach(s => context.Addresses.Add(s));
            context.SaveChanges();

            var studentGuardians = new List<StudentGuardian>
            {
                new StudentGuardian{StudentID=Guid.Parse("0132C0C3-CDC4-41F7-89E7-AA6C1452B58F"),GuardianID=Guid.Parse("E4F85065-AD5A-4122-8722-2CA89B514700")},
                new StudentGuardian{StudentID=Guid.Parse("0132C0C3-CDC4-41F7-89E7-AA6C1452B58F"),GuardianID=Guid.Parse("8519E1C1-466B-452D-9501-04CF5C9C1C09")}
            };

            studentGuardians.ForEach(s => context.StudentGuardians.Add(s));
            context.SaveChanges();

            var guardianAddress = new List<GuardianAddress>
            {
                new GuardianAddress{GuardianID=Guid.Parse("E4F85065-AD5A-4122-8722-2CA89B514700"), AddressID=Guid.Parse("B2BC45C2-CBDE-4036-B05A-0F9BE14A820C")},
                new GuardianAddress{GuardianID=Guid.Parse("8519E1C1-466B-452D-9501-04CF5C9C1C09"), AddressID=Guid.Parse("B2BC45C2-CBDE-4036-B05A-0F9BE14A820C")}
            };

            guardianAddress.ForEach(s => context.GuardianAddresses.Add(s));
            context.SaveChanges();
        }
    }
}