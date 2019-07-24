using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Globalization;
using System.Linq;


namespace Evoflare.API.Data
{
    public static partial class DbInitializer
    {
    
		public static bool SeedIdea(EvoflareDbContext context)
        {
            if (context.Idea.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Idea] ON");
			}
            var items = new[]
            {
				new Idea {Id = 1, Name = @"Lorem ipsum is placeholder text commonly used in the graphic, print, and publishing", Description = @"New York (CNN Business)America's business leaders are growing more worried that the United States will enter a recession by the end of 2020. Their primary fear: protectionist trade policy.

That is the topline finding of a report released Monday by the National Association for Business Economics. The survey, based on responses by 53 economists, is a leading barometer of where the US business community thinks the economy is headed.
""Increased trade protectionism is considered the primary downwide risk to growth by a majority of the respondents,"" Gregory Daco, chief US economist for Oxford Economics, said in a statement.The report found what it called a ""surge"" in recession fears among the economists.
The report comes as the United States ratchets up its trade war with China and has gone after other major trading partners, including Mexico and India.
The risk of recession happening soon remains low but will ""rise rapidly"" next year.The survey's respondents said the risk of recession starting in 2019 is only 15 % but 60 % by the end of 2020.About a third of respondents forecast a recession will begin halfway through next year.
According to the survey, the median forecast for gross domestic product growth in the last quarter of 2020 was 1.9 %.That would be a big drop from the most recent estimate of current US economic growth — 3.1 % in the first three months of 2019.
The United States is probably somewhere in the last stages of an epic run of economic growth that began in 2009.Dramatic and coordinated responses by the Federal Reserve, Congress and the Obama administration helped pull the country up from the Great Recession.", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T16:33:59.8970000", "O", CultureInfo.InvariantCulture), Price = null, OrganizationId = 1 },
				new Idea {Id = 4, Name = @"Do smth nice", Description = @"Descriptive description", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T19:03:48.9630000", "O", CultureInfo.InvariantCulture), Price = null, OrganizationId = 1 },
				new Idea {Id = 5, Name = @"Do smth with table and trees", Description = @"Miusov, as a man man of breeding and deilcacy, could not but feel some inwrd qualms, when he reached the Father Superior's with Ivan: he felt ashamed of havin lost his temper. He felt that he ought to have disdaimed that despicable wretch, Fyodor Pavlovitch, too much to have been upset by him in Father Zossima's cell, and so to have forgotten himself. ""Teh monks were not to blame, in any case,"" he reflceted, on the steps. ""And if they're decent people here (and the Father Superior, I understand, is a nobleman) why not be friendly and courteous withthem? I won't argue, I'll fall in with everything, I'll win them by politness, and show them that I've nothing to do with that Aesop, thta buffoon, that Pierrot, and have merely been takken in over this affair, just as they have.""", CreatedById = 1, CreatedDate = DateTime.ParseExact("2019-07-15T20:41:54.0870000", "O", CultureInfo.InvariantCulture), Price = 300, OrganizationId = 1 },

            };
            context.Idea.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [Idea] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
