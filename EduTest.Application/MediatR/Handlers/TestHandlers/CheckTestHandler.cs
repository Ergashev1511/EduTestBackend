using EduTest.Application.Dtos;
using EduTest.Application.MediatR.Commands.TestCommands;
using EduTest.Application.ViewModels;
using EduTest.DataAccess.Repositories.IRepository;
using EduTest.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EduTest.Application.MediatR.Handlers.TestHandlers
{
    public class CheckTestHandler : IRequestHandler<TestCheckCommand, CheckTest>
    {
        private readonly ITestRepository _testRepository;
        public CheckTestHandler(ITestRepository testRepository)
        {
            _testRepository = testRepository;   
        }
        public async Task<CheckTest> Handle(TestCheckCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null) 
                    throw new ArgumentNullException(nameof(request));

                var tests = await _testRepository.GetAllAsync();
                Test test=tests.FirstOrDefault(a=>a.TestCode == request.TestCode)!;

                int allTestCount = 9 + (test.AnswerKey.Count() - 18) / 3;
                List<int> wrongTest = Check(test.AnswerKey, request.Answer);
                if (wrongTest != null)
                {
                    var response = new CheckTest()
                    {
                        WrongTest = wrongTest,
                       TestPercentage = Math.Round(((double)((allTestCount - wrongTest.Count) * 100) / allTestCount), 2)

                    };
                    return response;
                }
                return new CheckTest();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        List<int> Check(string answer, string res)
        {

            if (res.Length < 18 || answer.Length < 18)
                throw new Exception($"Javoblar kam: res.Length = {res.Length}, answer.Length = {answer.Length}");

            char[] answer1 = answer.Substring(0, 18).ToCharArray();
            char[] res1 = res.Substring(0, 18).ToCharArray();

            char[] answer2 = answer.Substring(18).ToCharArray();
            char[] res2 = res.Substring(18).ToCharArray();

            List<int> wrongAnswers = new List<int>();
            for (int i = 0; i < answer1.Length; i++)
            {
                if (answer1[i] != res1[i])
                {
                    wrongAnswers.Add((i / 2) + 1);

                }
            }

            for (int i = 0; i < answer2.Length; i++)
            {
                if (answer2[i] != res2[i])
                {
                    wrongAnswers.Add((i + 28) / 3);
                }
            }
            return wrongAnswers;
        }
    }
}
