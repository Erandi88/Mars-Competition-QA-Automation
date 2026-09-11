using qa_dotnet_cucumber.Models;

namespace qa_dotnet_cucumber.Context
{
    public class TestDataContext
    {
        public List<EducationData> CreatedEducations { get; } = new();

        //how many eduaction rows exist(remember)
        public int EducationRowCountBeforeAction { get; set; }
    }
}