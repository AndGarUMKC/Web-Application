using CommerceBankApp.Areas.Identity.Data;
using CommerceBankApp.Controllers;
using CommerceBankApp.Data;
using CommerceBankApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net.Http;

namespace BankAppTesting.Moq
{
    public class OrganizationControllerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly OrganizationController _controller;

        public OrganizationControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var _context = new ApplicationDbContext(options);
            _userManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _controller = new OrganizationController(_context, _userManager.Object);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        private Organization GetOrganization(int id)
        {
            var org = new Organization()
            {
                OrganizationID = id,
                OrganizationName = "Hello",
                OrganizationDescription = "This is a test.",
                ImageUrl = "https://media.istockphoto.com/id/1270939459/vector/fundraising-round-ribbon-isolated-label-fundraising-sign.jpg?s=612x612&w=0&k=20&c=uUGQb0L8AdaHHR7pjk_kYWd587mnGv3gXc5OLHTK3Gk="

            };
            var httpContext = new DefaultHttpContext();
            _controller.TempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _controller.TempData["OrganizationName"] = org.OrganizationName;
            _controller.TempData["OrganizationImage"] = org.ImageUrl;

            return org;
        }

        private IEnumerable<Organization> GetOrganizations()
        {
            return new List<Organization>()
            {
                GetOrganization(0),
                GetOrganization(1),
                GetOrganization(2)
            };
        }

        [Fact]
        public async Task Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = await _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ShowSearchForm_ActionExecutes_ReturnsViewForShowSearchForm()
        {
            var result = await _controller.ShowSearchForm();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task ShowSearchResults_ActionExecutes_ReturnsViewForShowSearchResults()
        {
            var result = await _controller.ShowSearchResults("test");
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task UserResults_ActionExecutes_ReturnsViewForUserResults()
        {
            var result = await _controller.UserResults();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Details_IDisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_OrgisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Details(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_IDisFound_ReturnsViewResult()
        {
            await _controller.Create(GetOrganization(1));
            var result = await _controller.Details(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsViewForCreate()
        {

            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CreateAsync_ModelStateisValid_ReturnsRedirectToActionResultForCreate()
        {
            var result = await _controller.Create(GetOrganization(1));
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task CreateAsync_InvalidModelState_ReturnsView()
        {
            _controller.ModelState.AddModelError("OrganizationName", "Name is required");
            var org = new Organization
            {
                OrganizationID = 0,

                OrganizationDescription = "This is a test.",
                ImageUrl = "https://media.istockphoto.com/id/1270939459/vector/fundraising-round-ribbon-isolated-label-fundraising-sign.jpg?s=612x612&w=0&k=20&c=uUGQb0L8AdaHHR7pjk_kYWd587mnGv3gXc5OLHTK3Gk="

            };
            var result = await _controller.Create(org);

            var viewResult = Assert.IsType<ViewResult>(result);
            var testOrg = Assert.IsType<Organization>(viewResult.Model);

            Assert.Equal(org.DonationGoal, testOrg.DonationGoal);
            Assert.Equal(org.OrganizationID, testOrg.OrganizationID);
        }

        [Fact]
        public async Task Edit_ActionExecutes_ReturnsViewResultForEdit()
        {
            await _controller.Create(GetOrganization(1));
            var result = await _controller.Edit(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_IDisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Edit(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_OrgisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Edit(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditFull_ActionExecutes_ReturnsRedirectToActionResultForEdit()
        {
            var org = GetOrganization(1);
            await _controller.Create(org);
            var result = await _controller.Edit(1, org);
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task EditFull_IDisInvalid_ReturnsNotFoundResult()
        {
            var org = GetOrganization(1);
            await _controller.Create(org);
            var result = await _controller.Edit(2, org);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditFull_ModelisInvalid_ReturnsViewResultForEdit()
        {
            _controller.ModelState.AddModelError("OrganizationName", "Name is required");
            var org = new Organization
            {
                OrganizationID = 0,

                OrganizationDescription = "This is a test.",
                ImageUrl = "https://media.istockphoto.com/id/1270939459/vector/fundraising-round-ribbon-isolated-label-fundraising-sign.jpg?s=612x612&w=0&k=20&c=uUGQb0L8AdaHHR7pjk_kYWd587mnGv3gXc5OLHTK3Gk="

            };
            var result = await _controller.Edit(0,org);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_ActionExecutes_ReturnsViewResultForDelete()
        {
            var org = GetOrganization(1);
            await _controller.Create(org);
            var result = await _controller.Delete(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_IDisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Delete(null);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_OrgisNull_ReturnsNotFoundResult()
        {
            var result = await _controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_ActionExecutes_ReturnsRedirectToActionResultForDeleteConfirmed()
        {
            var org = GetOrganization(1);
            await _controller.Create(org);
            var result = await _controller.DeleteConfirmed(1);
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}