using NUnit.Framework;
using System;
using System.Reflection;
using domaindriven;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace TestProject
{
    [TestFixture]
    public class ReflectionTests
    {
        private Assembly assembly;
        private ConsoleApp consoleApp;
        private List<domaindriven.Models.Movie> testMovies;

        [OneTimeSetUp]
        public void LoadAssembly()
        {
            string assemblyPath = "domaindriven.dll"; // Adjust the path if needed
            assembly = Assembly.LoadFrom(assemblyPath);
        }

        [SetUp]
        public void Setup()
        {
            consoleApp = new ConsoleApp();
            testMovies = new List<domaindriven.Models.Movie>
            {
                new domaindriven.Models.Movie { Id = 1, Name = "Movie1", Year = 2020, Rating = 7.5 },
                new domaindriven.Models.Movie { Id = 2, Name = "Movie2", Year = 2019, Rating = 8.0 }
            };

            // Use reflection to set the private 'movies' field of ConsoleApp
            //Type type = typeof(ConsoleApp);
            string className = "domaindriven.ConsoleApp";
            Type type = assembly.GetType(className);
            FieldInfo fieldInfo = type.GetField("movies", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(consoleApp, testMovies);
        }

        [Test]
        public void ListMovies_NoMoviesFound()
        {
            // Arrange
            ClearMoviesList();

            // Act
            var listMoviesMethod = GetPrivateMethod("ListMovies");
            listMoviesMethod.Invoke(consoleApp, null);

            // Assert
            List<domaindriven.Models.Movie> moviesList = GetMoviesList();
            Assert.IsEmpty(moviesList);
        }

        [Test]
        public void ListMovies_MoviesFound()
        {
            // Arrange - No changes needed as setup already contains movies.

            // Act
            var listMoviesMethod = GetPrivateMethod("ListMovies");
            listMoviesMethod.Invoke(consoleApp, null);
            Assert.Pass();

        }
        
        [Test]
        public void Test_ConsoleAppListMoviesMethodExists()
        {
            Type consoleAppType = assembly.GetType("domaindriven.ConsoleApp");
            MethodInfo method = consoleAppType.GetMethod("ListMovies", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.NotNull(method, "The 'ListMovies' method should exist in the ConsoleApp class.");
        }

        [Test]
        public void Test_ConsoleAppFindMovieMethodExists()
        {
            Type consoleAppType = assembly.GetType("domaindriven.ConsoleApp");
            MethodInfo method = consoleAppType.GetMethod("FindMovie", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.NotNull(method, "The 'FindMovie' method should exist in the ConsoleApp class.");
        }

        [Test]
        public void Test_ConsoleAppAddMovieMethodExists()
        {
            Type consoleAppType = assembly.GetType("domaindriven.ConsoleApp");
            MethodInfo method = consoleAppType.GetMethod("AddMovie", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.NotNull(method, "The 'AddMovie' method should exist in the ConsoleApp class.");
        }

        [Test]
        public void Test_ConsoleAppEditMovieMethodExists()
        {
            Type consoleAppType = assembly.GetType("domaindriven.ConsoleApp");
            MethodInfo method = consoleAppType.GetMethod("EditMovie", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.NotNull(method, "The 'EditMovie' method should exist in the ConsoleApp class.");
        }

        [Test]
        public void Test_ConsoleAppDeleteMovieMethodExists()
        {
            Type consoleAppType = assembly.GetType("domaindriven.ConsoleApp");
            MethodInfo method = consoleAppType.GetMethod("DeleteMovie", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.NotNull(method, "The 'DeleteMovie' method should exist in the ConsoleApp class.");
        }


       



        [Test]
        public void Test_MovieClassExists()
        {
            string className = "domaindriven.Models.Movie";
            Type movieType = assembly.GetType(className);
            Assert.NotNull(movieType, $"The class '{className}' does not exist in the assembly.");
        }

        [Test]
        public void Test_ConsoleAppClassExists()
        {
            string className = "domaindriven.ConsoleApp";
            Type consoleAppType = assembly.GetType(className);
            Assert.NotNull(consoleAppType, $"The class '{className}' does not exist in the assembly.");
        }


        [Test]
        public void Test_MovieIdPropertyDataType()
        {
            Type movieType = assembly.GetType("domaindriven.Models.Movie");
            PropertyInfo property = movieType.GetProperty("Id");
            Assert.AreEqual("System.Int32", property.PropertyType.FullName, "The 'Id' property should be of type int.");
        }

        [Test]
        public void Test_MovieNamePropertyDataType()
        {
            Type movieType = assembly.GetType("domaindriven.Models.Movie");
            PropertyInfo property = movieType.GetProperty("Name");
            Assert.AreEqual("System.String", property.PropertyType.FullName, "The 'Name' property should be of type string.");
        }

        [Test]
        public void Test_MovieYearPropertyDataType()
        {
            Type movieType = assembly.GetType("domaindriven.Models.Movie");
            PropertyInfo property = movieType.GetProperty("Year");
            Assert.AreEqual("System.Int32", property.PropertyType.FullName, "The 'Year' property should be of type int.");
        }

        [Test]
        public void Test_MovieRatingPropertyDataType()
        {
            Type movieType = assembly.GetType("domaindriven.Models.Movie");
            PropertyInfo property = movieType.GetProperty("Rating");
            Assert.AreEqual("System.Double", property.PropertyType.FullName, "The 'Rating' property should be of type double.");
        }

        [Test]
        public void Test_MovieIdPropertyGetterSetter()
        {
            Type movieType = assembly.GetType("domaindriven.Models.Movie");
            PropertyInfo property = movieType.GetProperty("Id");
            Assert.IsTrue(property.CanRead, "The 'Id' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'Id' property should have a setter.");
        }

        [Test]
        public void Test_MovieNamePropertyGetterSetter()
        {
            Type movieType = assembly.GetType("domaindriven.Models.Movie");
            PropertyInfo property = movieType.GetProperty("Name");
            Assert.IsTrue(property.CanRead, "The 'Name' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'Name' property should have a setter.");
        }

        [Test]
        public void Test_MovieYearPropertyGetterSetter()
        {
            Type movieType = assembly.GetType("domaindriven.Models.Movie");
            PropertyInfo property = movieType.GetProperty("Year");
            Assert.IsTrue(property.CanRead, "The 'Year' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'Year' property should have a setter.");
        }

        [Test]
        public void Test_MovieRatingPropertyGetterSetter()
        {
            Type movieType = assembly.GetType("domaindriven.Models.Movie");
            PropertyInfo property = movieType.GetProperty("Rating");
            Assert.IsTrue(property.CanRead, "The 'Rating' property should have a getter.");
            Assert.IsTrue(property.CanWrite, "The 'Rating' property should have a setter.");
        }


        private List<domaindriven.Models.Movie> GetMoviesList()
        {
            string className = "domaindriven.ConsoleApp";
            Type type = assembly.GetType(className);
            FieldInfo fieldInfo = type.GetField("movies", BindingFlags.NonPublic | BindingFlags.Instance);
            return (List<domaindriven.Models.Movie>)fieldInfo.GetValue(consoleApp);
        }

        private void ClearMoviesList()
        {
            // Clear the 'movies' list using reflection
            //Type type = typeof(ConsoleApp);
            string className = "domaindriven.ConsoleApp";
            Type type = assembly.GetType(className);
            FieldInfo fieldInfo = type.GetField("movies", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(consoleApp, new List<domaindriven.Models.Movie>());
        }

        private MethodInfo GetPrivateMethod(string methodName, params Type[] parameterTypes)
        {
            //Type type = typeof(ConsoleApp);
            string className = "domaindriven.ConsoleApp";
            Type type = assembly.GetType(className);
            return type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        }


    }
}
