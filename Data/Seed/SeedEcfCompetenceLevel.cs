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
    
		public static bool SeedCompetenceLevel(EvoflareDbContext context)
        {
            if (context.CompetenceLevel.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CompetenceLevel] ON");
			}
            var items = new[]
            {
				new CompetenceLevel {Id = 1, CompetenceId = 1, Level = 4, Description = @"Provides leadership for the construction and implementation of long term innovative IS solutions." },
				new CompetenceLevel {Id = 2, CompetenceId = 1, Level = 5, Description = @"Provides IS strategic leadership to reach consensus and commitment from the management team of the enterprise." },
				new CompetenceLevel {Id = 3, CompetenceId = 2, Level = 3, Description = @"Ensures the content of the SLA." },
				new CompetenceLevel {Id = 4, CompetenceId = 2, Level = 4, Description = @"Negotiates revision of SLAs, in accordance with the overall objectives. Ensures the achievement of planned results." },
				new CompetenceLevel {Id = 5, CompetenceId = 3, Level = 3, Description = @"Exploits specialist knowledge to provide analysis of market environment etc." },
				new CompetenceLevel {Id = 6, CompetenceId = 3, Level = 4, Description = @"Provides leadership for the creation of an information system strategy that meets the requirements of the business (e.g. distributed, mobility-based) and includes risks and opportunities." },
				new CompetenceLevel {Id = 7, CompetenceId = 3, Level = 5, Description = @"Applies strategic thinking and organisational leadership to exploit the capability of Information Technology to improve the business." },
				new CompetenceLevel {Id = 8, CompetenceId = 4, Level = 2, Description = @"Acts systematically to document standard and simple elements of a product." },
				new CompetenceLevel {Id = 9, CompetenceId = 4, Level = 3, Description = @"Exploits specialist knowledge to create and maintain complex documents." },
				new CompetenceLevel {Id = 10, CompetenceId = 4, Level = 4, Description = @"Provides leadership and takes responsibility for, developing and maintaining overall plans." },
				new CompetenceLevel {Id = 11, CompetenceId = 5, Level = 3, Description = @"Exploits specialist knowledge to define relevant ICT technology and specifications to be deployed in the construction of multiple ICT projects, applications or infrastructure improvements." },
				new CompetenceLevel {Id = 12, CompetenceId = 5, Level = 4, Description = @"Acts with wide ranging accountability to define the strategy to implement ICT technology compliant with business need. Takes account of the current technology platform, obsolescent equipment and latest technological innovations." },
				new CompetenceLevel {Id = 13, CompetenceId = 5, Level = 5, Description = @"Provides ICT strategic leadership for implementing the enterprise strategy. Applies strategic thinking to discover and recognize new patterns in vast datasets and new ICT systems, to achieve business savings." },
				new CompetenceLevel {Id = 14, CompetenceId = 6, Level = 1, Description = @"Contributes to the design and general functional specification and interfaces." },
				new CompetenceLevel {Id = 15, CompetenceId = 6, Level = 2, Description = @"Organises the overall planning of the design of the application." },
				new CompetenceLevel {Id = 16, CompetenceId = 6, Level = 3, Description = @"Accounts for own and others actions in ensuring that the application is correctly integrated within a complex environment and complies with user / customer needs." },
				new CompetenceLevel {Id = 17, CompetenceId = 7, Level = 4, Description = @"Exploits wide ranging specialist knowledge of new and emerging technologies, coupled with a deep understanding of the business, to envision and articulate solutions for the future. Provides expert guidance and advice, to the leadership team to support strategic decisionmaking." },
				new CompetenceLevel {Id = 18, CompetenceId = 7, Level = 5, Description = @"Makes strategic decisions envisioning and articulating future ICT solutions for customer-oriented processes, new business products and services; directs the organisation to build and exploit them." },
				new CompetenceLevel {Id = 19, CompetenceId = 8, Level = 3, Description = @"Promotes awareness, training and commitment for the deployment of sustainable development and applies the necessary tools for piloting this approach." },
				new CompetenceLevel {Id = 20, CompetenceId = 8, Level = 4, Description = @"Defines objective and strategy of sustainable IS development in accordance with the organisation’s sustainability policy." },
				new CompetenceLevel {Id = 21, CompetenceId = 9, Level = 4, Description = @"Applies independent thinking and technology awareness to lead the integration of disparate concepts for the provision of unique solutions." },
				new CompetenceLevel {Id = 22, CompetenceId = 9, Level = 5, Description = @"Challenges the status quo and provides strategic leadership for the introduction of revolutionary concepts." },
				new CompetenceLevel {Id = 23, CompetenceId = 10, Level = 1, Description = @"Acts under guidance to develop, test and document applications." },
				new CompetenceLevel {Id = 24, CompetenceId = 10, Level = 2, Description = @"Systematically develops and validates applications." },
				new CompetenceLevel {Id = 25, CompetenceId = 10, Level = 3, Description = @"Acts creatively to develop applications and to select appropriate technical options. Accounts for others development activities. Optimizes application development, maintenance and performance by employing design patterns and by reusing proved solutions." },
				new CompetenceLevel {Id = 26, CompetenceId = 11, Level = 2, Description = @"Acts systematically to identify compatibility of software and hardware specifications. Documents all activities during installation and records deviations and remedial activities." },
				new CompetenceLevel {Id = 27, CompetenceId = 11, Level = 3, Description = @"Accounts for own and others actions in the integration process. Complies with appropriate standards and change control procedures to maintain integrity of the overall system functionality and reliability." },
				new CompetenceLevel {Id = 28, CompetenceId = 11, Level = 4, Description = @"Exploits wide ranging specialist knowledge to create a process for the entire integration cycle, including the establishment of internal standards of practice. Provides leadership to marshal and assign resources for programmes of integration." },
				new CompetenceLevel {Id = 29, CompetenceId = 12, Level = 1, Description = @"Performs simple tests in strict compliance with detailed instructions." },
				new CompetenceLevel {Id = 30, CompetenceId = 12, Level = 2, Description = @"Organises test programmes and builds scripts to stress test potential vulnerabilities. Records and reports outcomes providing analysis of results." },
				new CompetenceLevel {Id = 31, CompetenceId = 12, Level = 3, Description = @"Exploits specialist knowledge to supervise complex testing programmes. Ensures tests and results are documented to provide input to subsequent process owners such as designers, users or maintainers. Accountable for compliance with testing procedures including a documented audit trail." },
				new CompetenceLevel {Id = 32, CompetenceId = 12, Level = 4, Description = @"Exploits wide ranging specialist knowledge to create a process for the entire testing activity, including the establishment of internal standard of practices. Provides expert guidance and advice to the testing team." },
				new CompetenceLevel {Id = 33, CompetenceId = 13, Level = 1, Description = @"Removes or installs components under guidance and in accordance with detailed instructions." },
				new CompetenceLevel {Id = 34, CompetenceId = 13, Level = 2, Description = @"Acts systematically to build or deconstruct system elements. Identifies failing components and establishes root cause failures. Provides support to less experienced colleagues." },
				new CompetenceLevel {Id = 35, CompetenceId = 13, Level = 3, Description = @"Accounts for own and others actions for solution provision and initiates comprehensive communication with stakeholders. Exploits specialist knowledge to influence solution construction providing advice and guidance." },
				new CompetenceLevel {Id = 36, CompetenceId = 14, Level = 1, Description = @"Uses and applies standards to define document structure." },
				new CompetenceLevel {Id = 37, CompetenceId = 14, Level = 2, Description = @"Determines documentation requirements taking into account the purpose and environment to which it applies." },
				new CompetenceLevel {Id = 38, CompetenceId = 14, Level = 3, Description = @"Adapts the level of detail according to the objective of the documentation and the targeted population." },
				new CompetenceLevel {Id = 39, CompetenceId = 15, Level = 3, Description = @"Ensures interoperability of the system components. Exploits wide ranging specialist knowledge to create a complete system that will satisfy the system constraints and meet the customer’s expectations." },
				new CompetenceLevel {Id = 40, CompetenceId = 15, Level = 4, Description = @"Handles complexity by developing standard procedures and architectures in support of cohesive product development. Establishes a set of system requirements that will guide the design of the system. Identifies which system requirements should be allocated to which elements of the system." },
				new CompetenceLevel {Id = 41, CompetenceId = 16, Level = 1, Description = @"Interacts with users, applies basic product knowledge to respond to user requests. Solves incidents, following prescribed procedures." },
				new CompetenceLevel {Id = 42, CompetenceId = 16, Level = 2, Description = @"Systematically interprets user problems and identifies solutions and possible side effects. Uses experience to address user problems and interrogates database for potential solutions. Escalates complex or unresolved incidents. Records and tracks issues from outset to conclusion." },
				new CompetenceLevel {Id = 43, CompetenceId = 16, Level = 3, Description = @"Manages the support process and accountable for agreed SLA. Plans resource allocation to meet defined service level. Acts creatively, and applies continuous service improvement. Manages the support function budget." },
				new CompetenceLevel {Id = 44, CompetenceId = 17, Level = 2, Description = @"During change, acts systematically to respond to day by day operational needs and react to them, avoiding service disruptions and maintaining coherence to (SLA) and information security requirements." },
				new CompetenceLevel {Id = 45, CompetenceId = 17, Level = 3, Description = @"Ensures the integrity of the system by controlling the application of functional updates, software or hardware additions and maintenance activities.Complies with budget requirements." },
				new CompetenceLevel {Id = 46, CompetenceId = 18, Level = 1, Description = @"Acts under guidance to record and track reliability data." },
				new CompetenceLevel {Id = 47, CompetenceId = 18, Level = 2, Description = @"Systematically analyses performance data and communicates findings to senior experts.Escalates potential service level failures and security risks, recommends actions to improve service reliability.Tracks reliability data against SLA." },
				new CompetenceLevel {Id = 48, CompetenceId = 18, Level = 3, Description = @"Programmes the schedule of operational tasks.Manages costs and budget according to the internal procedures and external constraints.Identifies the optimum number of people required to resource the operational management of the IS infrastructure." },
				new CompetenceLevel {Id = 49, CompetenceId = 19, Level = 2, Description = @"Identifies and classifies incident types and service interruptions. Records incidents cataloguing them by symptom and resolution." },
				new CompetenceLevel {Id = 50, CompetenceId = 19, Level = 3, Description = @"Exploits specialist knowledge and in-depth understanding of the ICT infrastructure and problem management process to identify failures and resolve with minimum outage. Makes sound decisions in emotionally charged environments on appropriate action required to minimise business impact. Rapidly identifies failing component, selects alternatives such as repair, replace or reconfigure." },
				new CompetenceLevel {Id = 51, CompetenceId = 19, Level = 4, Description = @"Provides leadership and is accountable for the entire problem management process. Schedules and ensures well trained human resources, tools, and diagnostic equipment are available to meet emergency incidents. Has depth of expertise to anticipate critical component failure and make provision for recovery with minimum downtime. Constructs escalation processes to ensure that appropriate resources can be applied to each incident." },
				new CompetenceLevel {Id = 52, CompetenceId = 20, Level = 4, Description = @"Exploits depth of expertise and leverages external standards and best practices." },
				new CompetenceLevel {Id = 53, CompetenceId = 20, Level = 5, Description = @"Provides strategic leadership to embed information security into the culture of the organisation." },
				new CompetenceLevel {Id = 54, CompetenceId = 24, Level = 4, Description = @"Exploits wide ranging specialist knowledge to leverage and authorise the application of external standards and best practices." },
				new CompetenceLevel {Id = 55, CompetenceId = 24, Level = 5, Description = @"Provides strategic leadership to embed ICT quality (i.e. metrics and continuous improvement) into the culture of the organisation." },
				new CompetenceLevel {Id = 56, CompetenceId = 25, Level = 2, Description = @"Organises the identification of training needs; collates organisation requirements, identifies, selects and prepares schedule of training interventions." },
				new CompetenceLevel {Id = 57, CompetenceId = 25, Level = 3, Description = @"Acts creatively to analyse skills gaps; elaborates specific requirements and identifies potential sources for training provision. Has specialist knowledge of the training market and establishes a feedback mechanism to assess the added value of alternative training programmes." },
				new CompetenceLevel {Id = 58, CompetenceId = 26, Level = 2, Description = @"Understands and applies the principles of the procurement process; places orders based on existing supplier contracts. Ensures the correct execution of orders, including validation of deliverables and correlation with subsequent payments." },
				new CompetenceLevel {Id = 59, CompetenceId = 26, Level = 3, Description = @"Exploits specialist knowledge to deploy the purchasing process, ensuring positive commercial relationships with suppliers. Selects suppliers, products and services by evaluating performance, cost, timeliness and quality. Decides contract placement and complies with organisational policies." },
				new CompetenceLevel {Id = 60, CompetenceId = 26, Level = 4, Description = @"Provides leadership for the application of the organisation’s procurement policies and makes recommendations for process enhancement. Applies experience and procurement practice expertise to make ultimate purchasing decisions." },
				new CompetenceLevel {Id = 61, CompetenceId = 27, Level = 2, Description = @"Organises collaboration between relevant internal departments, for example, technical, sales and legal. Facilitates comparison between customer requirement and available ‘off the shelf’ solutions." },
				new CompetenceLevel {Id = 62, CompetenceId = 27, Level = 3, Description = @"Acts creatively to develop proposal incorporating a complex solution. Customises solution in a complex technical and legal environment and ensures feasibility, legal and technical validity of customer offer." },
				new CompetenceLevel {Id = 63, CompetenceId = 28, Level = 3, Description = @"Acts creatively to influence the establishment of a VAR network. Manages the identification and assessment of potential VAR members and sets up support procedures. VARs managed to maximise business performance." },
				new CompetenceLevel {Id = 64, CompetenceId = 28, Level = 4, Description = @"Exploits wide ranging skills in marketing and sales to create the organisation’s VAR strategy. Establishes the processes by which VARs will be managed to maximise business performance." },
				new CompetenceLevel {Id = 65, CompetenceId = 29, Level = 3, Description = @"Contributes to the sales process by effectively presenting products or services to customers." },
				new CompetenceLevel {Id = 66, CompetenceId = 29, Level = 4, Description = @"Assesses and estimates appropriate sales strategies to deliver company results. Decides and allocates annual sales targets and adjusts incentives to meet market conditions." },
				new CompetenceLevel {Id = 67, CompetenceId = 29, Level = 5, Description = @"Assumes ultimate responsibility for the sales performance of the organisation. Authorises resource allocation, prioritises product and service promotions, advises board directors of sales performance." },
				new CompetenceLevel {Id = 68, CompetenceId = 30, Level = 2, Description = @"Acts systematically to monitor contract compliance and promptly escalate defaults." },
				new CompetenceLevel {Id = 69, CompetenceId = 30, Level = 3, Description = @"Evaluates contract performance by monitoring performance indicators. Assures performance of the complete supply chain. Influences the terms of contract renewal." },
				new CompetenceLevel {Id = 70, CompetenceId = 30, Level = 4, Description = @"Provides leadership for contract compliance and is the final escalation point for issue resolution." },
				new CompetenceLevel {Id = 71, CompetenceId = 31, Level = 2, Description = @"Briefs/ trains individuals and groups, holds courses of instruction." },
				new CompetenceLevel {Id = 72, CompetenceId = 31, Level = 3, Description = @"Monitors and addressees the development needs of individuals and teams." },
				new CompetenceLevel {Id = 73, CompetenceId = 31, Level = 4, Description = @"Takes proactive action and develops organisational processes to address the development needs of individuals, teams and the entire workforce." },
				new CompetenceLevel {Id = 74, CompetenceId = 21, Level = 3, Description = @"Analyses business processes and associated information requirements and provides the most appropriate information structure." },
				new CompetenceLevel {Id = 75, CompetenceId = 21, Level = 4, Description = @"Integrates the appropriate information structure into the corporate environment." },
				new CompetenceLevel {Id = 76, CompetenceId = 21, Level = 5, Description = @"Correlates information and knowledge to create value for the business. Applies innovative solutions based on information retrieved." },
				new CompetenceLevel {Id = 77, CompetenceId = 22, Level = 3, Description = @"Establishes reliable relationships with customers and helps them clarify their needs." },
				new CompetenceLevel {Id = 78, CompetenceId = 22, Level = 4, Description = @"Exploits wide ranging specialist knowledge of the customers business to offer possible solutions to business needs. Provides expert guidance to the customer by proposing solutions and supplier." },
				new CompetenceLevel {Id = 79, CompetenceId = 22, Level = 5, Description = @"Provides leadership in support of the customers’ strategic decisions. Helps customer to envisage new ICT solutions, fosters partnerships and creates value propositions." },
				new CompetenceLevel {Id = 80, CompetenceId = 23, Level = 2, Description = @"Understands and applies digital marketing tactics to develop an integrated and effective digital marketing plan using different digital marketing areas such as search, display, e-mail, social media and mobile marketing." },
				new CompetenceLevel {Id = 81, CompetenceId = 23, Level = 3, Description = @"Exploits specialist knowledge to utilise analytical tools and assess the effectiveness of websites in terms of technical performance and download speed. Evaluates the user engagement by the application of a wide range of analytical reports. Knows the legal implications of the approaches adopted." },
				new CompetenceLevel {Id = 82, CompetenceId = 23, Level = 4, Description = @"Develops clear meaningful objectives for the Digital Marketing Plan. Selects appropriate tools and sets budget targets for the channels adopted. Monitors, analyses and enhances the digital marketing activities in an ongoing manner." },
				new CompetenceLevel {Id = 83, CompetenceId = 32, Level = 3, Description = @"Exploits skills to provide short-term forecast using market inputs and assessing the organisation’s production and selling capabilities." },
				new CompetenceLevel {Id = 84, CompetenceId = 32, Level = 4, Description = @"Acts with wide ranging accountability for the production of a long-term forecast. Understands the global marketplace, identifying and evaluating relevant inputs from the broader business, political and social context." },
				new CompetenceLevel {Id = 85, CompetenceId = 33, Level = 2, Description = @"Understands and applies the principles of project management and applies methodologies, tools and processes to manage simple projects, Optimises costs and minimises waste." },
				new CompetenceLevel {Id = 86, CompetenceId = 33, Level = 3, Description = @"Accounts for own and others activities, working within the project boundary, making choices and giving instructions, optimising activities and resources. Manages and supervises relationships within the team; plans and establishes team objectives and outputs and documents results." },
				new CompetenceLevel {Id = 87, CompetenceId = 33, Level = 4, Description = @"Manages complex projects or programmes, including interaction with others. Influences project strategy by proposing new or alternative solutions and balancing effectiveness and efficiency. Is empowered to revise rules and choose standards. Takes overall responsibility for project outcomes, including finance and resource management and works beyond project boundary." },
				new CompetenceLevel {Id = 88, CompetenceId = 33, Level = 5, Description = @"Provides strategic leadership for extensive interrelated programmes of work to ensure that Information Technology is a change enabling agent and delivers benefit in line with overall business strategic aims. Applies extensive business and technological mastery to conceive and bring innovative ideas to fruition." },
				new CompetenceLevel {Id = 89, CompetenceId = 34, Level = 2, Description = @"Understands and applies the principles of risk management and investigates ICT solutions to mitigate identified risks." },
				new CompetenceLevel {Id = 90, CompetenceId = 34, Level = 3, Description = @"Decides on appropriate actions required to adapt security and address risk exposure. Evaluates, manages and ensures validation of exceptions; audits ICT processes and environment." },
				new CompetenceLevel {Id = 91, CompetenceId = 34, Level = 4, Description = @"Provides leadership to define and make applicable a policy for risk management by considering all the possible constraints, including technical, economic and political issues. Delegates assignments." },
				new CompetenceLevel {Id = 92, CompetenceId = 35, Level = 3, Description = @"Accounts for own and others actions in managing a limited number of stakeholders." },
				new CompetenceLevel {Id = 93, CompetenceId = 35, Level = 4, Description = @"Provides leadership for large or many stakeholder relationships. Authorises investment in new and existing relationships. Leads the design of a workable procedure for maintaining positive business relationships." },
				new CompetenceLevel {Id = 94, CompetenceId = 36, Level = 3, Description = @"Exploits specialist knowledge to research existing ICT processes and solutions in order to define possible innovations. Makes recommendations based on reasoned arguments." },
				new CompetenceLevel {Id = 95, CompetenceId = 36, Level = 4, Description = @"Provides leadership and authorises implementation of innovations and improvements that will enhance competitiveness or efficiency. Demonstrates to senior management the business advantage of potential changes." },
				new CompetenceLevel {Id = 96, CompetenceId = 37, Level = 2, Description = @"Communicates and monitors application of the organisation’s quality policy." },
				new CompetenceLevel {Id = 97, CompetenceId = 37, Level = 3, Description = @"Evaluates quality management indicators and processes based on ICT quality policy and proposes remedial action." },
				new CompetenceLevel {Id = 98, CompetenceId = 37, Level = 4, Description = @"Assesses and estimates the degree to which quality requirements have been met and provides leadership for quality policy implementation. Provides cross functional leadership for setting and exceeding quality standards." },
				new CompetenceLevel {Id = 99, CompetenceId = 38, Level = 3, Description = @"Evaluates change requirements and exploits specialist skills to identify possible methods and standards that can be deployed." },
				new CompetenceLevel {Id = 100, CompetenceId = 38, Level = 4, Description = @"Provides leadership to plan, manage and implement significant ICT led business change." },
				new CompetenceLevel {Id = 101, CompetenceId = 38, Level = 5, Description = @"Applies pervasive influence to embed organisational change." },
				new CompetenceLevel {Id = 102, CompetenceId = 39, Level = 2, Description = @"Systematically scans the environment to identify and define vulnerabilities and threats. Records and escalates noncompliance." },
				new CompetenceLevel {Id = 103, CompetenceId = 39, Level = 3, Description = @"Evaluates security management measures and indicators and decides if compliant to information security policy. Investigates and instigates remedial measures to address any security breaches." },
				new CompetenceLevel {Id = 104, CompetenceId = 39, Level = 4, Description = @"Provides leadership for the integrity, confidentiality and availability of data stored on information systems and complies with all legal requirements." },
				new CompetenceLevel {Id = 105, CompetenceId = 40, Level = 4, Description = @"Provides leadership for IS governance strategy by communicating, propagating and controlling relevant processes across the entire ICT infrastructure." },
				new CompetenceLevel {Id = 106, CompetenceId = 40, Level = 5, Description = @"Defines and aligns the IS governance strategy incorporating it into the organisation’s corporate governance strategy. Adapts the IS governance strategy to take into account new significant events arising from legal, economic, political, business, technological or environmental issues." },

            };
            context.CompetenceLevel.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [CompetenceLevel] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
