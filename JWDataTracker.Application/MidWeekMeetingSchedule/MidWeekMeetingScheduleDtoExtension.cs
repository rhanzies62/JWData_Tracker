using JWDataTracker.Helper;

namespace JWDataTracker.Application.MidWeekMeetingSchedule
{
    public static class MidWeekMeetingScheduleDtoExtension
    {
        public static Response Validate(this MidWeekMeetingScheduleDto model)
        {
            var categories = new List<int>() {
                0,1,2,3,4
            };
            var response = new Response(true, String.Empty);
            categories.ForEach(category =>
            {
                var schedules = model.MidWeekScheduleItems.Where(i => i.Category == category && i.PublisherId != 0).ToList();
                schedules.ForEach(sched =>
                {
                    var count = schedules.Count(s => s.PublisherId == sched.PublisherId);
                    if (count > 1)
                        response = new Response(false, $"{sched.PublisherName} can't appear multiple times in {category}");

                    var roleCount = schedules.Count(s => s.Role == sched.Role);
                    if (roleCount > 1)
                        response = new Response(false, $"{sched.Role} can't appear multiple times in {category}");
                });
            });

            //if (response.IsSuccess)
            //{
            //    var tsgw = model.MidWeekScheduleItems.Where(i => i.Category == Constant.TREASUREFROMGODSWORD && i.PublisherId != 0).ToList();
            //    var lac = model.MidWeekScheduleItems.Where(i => i.Category == Constant.LIVINGASACHRISTIAN && i.PublisherId != 0).ToList();

            //    response = ValidateExistence(tsgw, lac, "{0} already has a part in {1} and can't appear again in {2}", Constant.TreasureFromGodsWord, Constant.LivingAsAChristian);
            //    if(response.IsSuccess) response = ValidateExistence(lac, tsgw, "{0} already has a part in {1} and can't appear again in {2}", Constant.LivingAsAChristian, Constant.TreasureFromGodsWord);
            //}

            if (response.IsSuccess)
            {
                var ayttfm = model.MidWeekScheduleItems.Where(i => i.Category == 2 && i.PartnerPublisherId != 0).ToList();
                ayttfm.ForEach(sched =>
                {
                    var count = ayttfm.Count(s => s.PartnerPublisherId == sched.PartnerPublisherId);
                    if (count > 1)
                        response = new Response(false, $"{sched.PublisherName} can't appear in multiple times in {Constant.ApplyYourselfToTheFieldMinistry}");
                });
            }

            return response;
        }

        private static Response ValidateExistence(List<MidWeekScheduleItemDto> lst1, List<MidWeekScheduleItemDto> lst2, string errMsg, string part1, string part2)
        {
            var response = new Response(true, string.Empty);
            lst1.ForEach(t =>
            {
                if (lst2.Any(l => t.PublisherId == l.PublisherId))
                    response = new Response(false, string.Format(errMsg, t.PublisherName, part1, part2));
            });

            return response;
        }
    }
}
