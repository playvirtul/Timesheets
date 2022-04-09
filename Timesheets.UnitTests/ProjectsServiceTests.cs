namespace Timesheets.UnitTests
{
    public class ProjectsServiceTests
    {
        //[Fact]
        //public async Task Create_ShouldCreateValidProject()
        //{
        //    //arrange
        //    var service = new ProjectsService();
        //    var fixture = new Fixture();
        //    var isContains = false;

        //    Projects.ProjectsList.Clear();

        //    var project = fixture.Build<Project>()
        //        .Without(x => x.WorkingHours)
        //        .Create();

        //    //act
        //    var result = await service.Create(project);
        //    isContains = Projects.ProjectsList.Contains(project);

        //    //assert
        //    Assert.True(isContains);
        //    Assert.True(result);
        //}

        //[Theory]
        //[InlineData(0, "Test")]
        //[InlineData(-10, "Test")]
        //[InlineData(1, " ")]
        //[InlineData(1, "")]
        //[InlineData(1, null)]
        //public async Task Create_InValidProject_ShouldThrowArgumentException(int id, string name)
        //{
        //    //arrange
        //    var service = new ProjectsService();
        //    var project = new Project
        //    {
        //        Id = id,
        //        EmployeeName = name
        //    };

        //    //act

        //    //assert
        //    await Assert.ThrowsAsync<ArgumentException>(() => service.Create(project));
        //}

        //[Fact]
        //public async Task Delete_ShouldDeleteProject()
        //{
        //    //arrange
        //    var project = new Project
        //    {
        //        Id = 1,
        //        EmployeeName = "Test"
        //    };

        //    Projects.ProjectsList.Clear();

        //    Projects.ProjectsList.Add(project);

        //    var service = new ProjectsService();
        //    var isContains = true;

        //    //act
        //    var result = await service.Delete(project.Id);
        //    isContains = Projects.ProjectsList.Contains(project);

        //    //assert
        //    Assert.True(result);
        //    Assert.False(isContains);
        //}

        //[Theory]
        //[InlineData(0)]
        //[InlineData(-100)]
        //public async Task Delete_InValidId_ShouldThrowArgumentException(int id)
        //{
        //    //arrange
        //    var service = new ProjectsService();

        //    //act

        //    //assert
        //    await Assert.ThrowsAsync<ArgumentException>(() => service.Delete(id));
        //}

        //[Fact]
        //public async Task AddWorkingHours_ShouldAddHours()
        //{
        //    //arrange
        //    int hours = 10;
        //    var isAddedHours = false;

        //    var project = new Project
        //    {
        //        Id = 1,
        //        EmployeeName = "Test"
        //    };

        //    Projects.ProjectsList.Clear();

        //    Projects.ProjectsList.Add(project);

        //    var service = new ProjectsService();

        //    //act
        //    var result = await service.AddWorkingHours(project.EmployeeName, hours);
        //    isAddedHours = Projects.ProjectsList.FirstOrDefault(x => x.Id == project.Id).WorkingHours == hours;

        //    //assert
        //    Assert.True(result);
        //    Assert.True(isAddedHours);
        //}

        //[Theory]
        //[InlineData("", 10)]
        //[InlineData(" ", 10)]
        //[InlineData(null, 10)]
        //[InlineData("Test", 0)]
        //[InlineData("Test", -100)]
        //public async Task AddWorkingHours_InValidNameOrHours_ShouldThrowArgumentException(int projectId, int hours)
        //{
        //    //arrange
        //    var service = new ProjectsService();

        //    //act

        //    //assert
        //    await Assert.ThrowsAsync<ArgumentException>(() => service.AddWorkingHours(projectId, hours));
        //}
    }
}