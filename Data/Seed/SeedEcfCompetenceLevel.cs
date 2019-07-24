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
    
		public static bool SeedEcfCompetenceLevel(EvoflareDbContext context)
        {
            if (context.EcfCompetenceLevel.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfCompetenceLevel] ON");
			}
            var items = new[]
            {
				new EcfCompetenceLevel {Id = 1, CompetenceId = @"A1 ", Level = 4, Description = @"Provides leadership for the construction and implementation of long term innovative IS solutions." },
				new EcfCompetenceLevel {Id = 2, CompetenceId = @"A1 ", Level = 5, Description = @"Provides IS strategic leadership to reach consensus and commitment from the management team of the enterprise." },
				new EcfCompetenceLevel {Id = 3, CompetenceId = @"A2 ", Level = 3, Description = @"Ensures the content of the SLA." },
				new EcfCompetenceLevel {Id = 4, CompetenceId = @"A2 ", Level = 4, Description = @"Negotiates revision of SLAs, in accordance with the overall objectives. Ensures the achievement of planned results." },
				new EcfCompetenceLevel {Id = 5, CompetenceId = @"A3 ", Level = 3, Description = @"Exploits specialist knowledge to provide analysis of market environment etc." },
				new EcfCompetenceLevel {Id = 6, CompetenceId = @"A3 ", Level = 4, Description = @"Provides leadership for the creation of an information system strategy that meets the requirements of the business (e.g. distributed, mobility-based) and includes risks and opportunities." },
				new EcfCompetenceLevel {Id = 7, CompetenceId = @"A3 ", Level = 5, Description = @"Applies strategic thinking and organisational leadership to exploit the capability of Information Technology to improve the business." },
				new EcfCompetenceLevel {Id = 8, CompetenceId = @"A4 ", Level = 2, Description = @"Acts systematically to document standard and simple elements of a product." },
				new EcfCompetenceLevel {Id = 9, CompetenceId = @"A4 ", Level = 3, Description = @"Exploits specialist knowledge to create and maintain complex documents." },
				new EcfCompetenceLevel {Id = 10, CompetenceId = @"A4 ", Level = 4, Description = @"Provides leadership and takes responsibility for, developing and maintaining overall plans." },
				new EcfCompetenceLevel {Id = 11, CompetenceId = @"A5 ", Level = 3, Description = @"Exploits specialist knowledge to define relevant ICT technology and specifications to be deployed in the construction of multiple ICT projects, applications or infrastructure improvements." },
				new EcfCompetenceLevel {Id = 12, CompetenceId = @"A5 ", Level = 4, Description = @"Acts with wide ranging accountability to define the strategy to implement ICT technology compliant with business need. Takes account of the current technology platform, obsolescent equipment and latest technological innovations." },
				new EcfCompetenceLevel {Id = 13, CompetenceId = @"A5 ", Level = 5, Description = @"Provides ICT strategic leadership for implementing the enterprise strategy. Applies strategic thinking to discover and recognize new patterns in vast datasets and new ICT systems, to achieve business savings." },
				new EcfCompetenceLevel {Id = 14, CompetenceId = @"A6 ", Level = 1, Description = @"Contributes to the design and general functional specification and interfaces." },
				new EcfCompetenceLevel {Id = 15, CompetenceId = @"A6 ", Level = 2, Description = @"Organises the overall planning of the design of the application." },
				new EcfCompetenceLevel {Id = 16, CompetenceId = @"A6 ", Level = 3, Description = @"Accounts for own and others actions in ensuring that the application is correctly integrated within a complex environment and complies with user / customer needs." },
				new EcfCompetenceLevel {Id = 17, CompetenceId = @"A7 ", Level = 4, Description = @"Exploits wide ranging specialist knowledge of new and emerging technologies, coupled with a deep understanding of the business, to envision and articulate solutions for the future. Provides expert guidance and advice, to the leadership team to support strategic decisionmaking." },
				new EcfCompetenceLevel {Id = 18, CompetenceId = @"A7 ", Level = 5, Description = @"Makes strategic decisions envisioning and articulating future ICT solutions for customer-oriented processes, new business products and services; directs the organisation to build and exploit them." },
				new EcfCompetenceLevel {Id = 19, CompetenceId = @"A8 ", Level = 3, Description = @"Promotes awareness, training and commitment for the deployment of sustainable development and applies the necessary tools for piloting this approach." },
				new EcfCompetenceLevel {Id = 20, CompetenceId = @"A8 ", Level = 4, Description = @"Defines objective and strategy of sustainable IS development in accordance with the organisation’s sustainability policy." },
				new EcfCompetenceLevel {Id = 21, CompetenceId = @"A9 ", Level = 4, Description = @"Applies independent thinking and technology awareness to lead the integration of disparate concepts for the provision of unique solutions." },
				new EcfCompetenceLevel {Id = 22, CompetenceId = @"A9 ", Level = 5, Description = @"Challenges the status quo and provides strategic leadership for the introduction of revolutionary concepts." },
				new EcfCompetenceLevel {Id = 23, CompetenceId = @"B1 ", Level = 1, Description = @"Acts under guidance to develop, test and document applications." },
				new EcfCompetenceLevel {Id = 24, CompetenceId = @"B1 ", Level = 2, Description = @"Systematically develops and validates applications." },
				new EcfCompetenceLevel {Id = 25, CompetenceId = @"B1 ", Level = 3, Description = @"Acts creatively to develop applications and to select appropriate technical options. Accounts for others development activities. Optimizes application development, maintenance and performance by employing design patterns and by reusing proved solutions." },
				new EcfCompetenceLevel {Id = 26, CompetenceId = @"B2 ", Level = 2, Description = @"Acts systematically to identify compatibility of software and hardware specifications. Documents all activities during installation and records deviations and remedial activities." },
				new EcfCompetenceLevel {Id = 27, CompetenceId = @"B2 ", Level = 3, Description = @"Accounts for own and others actions in the integration process. Complies with appropriate standards and change control procedures to maintain integrity of the overall system functionality and reliability." },
				new EcfCompetenceLevel {Id = 28, CompetenceId = @"B2 ", Level = 4, Description = @"Exploits wide ranging specialist knowledge to create a process for the entire integration cycle, including the establishment of internal standards of practice. Provides leadership to marshal and assign resources for programmes of integration." },
				new EcfCompetenceLevel {Id = 29, CompetenceId = @"B3 ", Level = 1, Description = @"Performs simple tests in strict compliance with detailed instructions." },
				new EcfCompetenceLevel {Id = 30, CompetenceId = @"B3 ", Level = 2, Description = @"Organises test programmes and builds scripts to stress test potential vulnerabilities. Records and reports outcomes providing analysis of results." },
				new EcfCompetenceLevel {Id = 31, CompetenceId = @"B3 ", Level = 3, Description = @"Exploits specialist knowledge to supervise complex testing programmes. Ensures tests and results are documented to provide input to subsequent process owners such as designers, users or maintainers. Accountable for compliance with testing procedures including a documented audit trail." },
				new EcfCompetenceLevel {Id = 32, CompetenceId = @"B3 ", Level = 4, Description = @"Exploits wide ranging specialist knowledge to create a process for the entire testing activity, including the establishment of internal standard of practices. Provides expert guidance and advice to the testing team." },
				new EcfCompetenceLevel {Id = 33, CompetenceId = @"B4 ", Level = 1, Description = @"Removes or installs components under guidance and in accordance with detailed instructions." },
				new EcfCompetenceLevel {Id = 34, CompetenceId = @"B4 ", Level = 2, Description = @"Acts systematically to build or deconstruct system elements. Identifies failing components and establishes root cause failures. Provides support to less experienced colleagues." },
				new EcfCompetenceLevel {Id = 35, CompetenceId = @"B4 ", Level = 3, Description = @"Accounts for own and others actions for solution provision and initiates comprehensive communication with stakeholders. Exploits specialist knowledge to influence solution construction providing advice and guidance." },
				new EcfCompetenceLevel {Id = 36, CompetenceId = @"B5 ", Level = 1, Description = @"Uses and applies standards to define document structure." },
				new EcfCompetenceLevel {Id = 37, CompetenceId = @"B5 ", Level = 2, Description = @"Determines documentation requirements taking into account the purpose and environment to which it applies." },
				new EcfCompetenceLevel {Id = 38, CompetenceId = @"B5 ", Level = 3, Description = @"Adapts the level of detail according to the objective of the documentation and the targeted population." },
				new EcfCompetenceLevel {Id = 39, CompetenceId = @"B6 ", Level = 3, Description = @"Ensures interoperability of the system components. Exploits wide ranging specialist knowledge to create a complete system that will satisfy the system constraints and meet the customer’s expectations." },
				new EcfCompetenceLevel {Id = 40, CompetenceId = @"B6 ", Level = 4, Description = @"Handles complexity by developing standard procedures and architectures in support of cohesive product development. Establishes a set of system requirements that will guide the design of the system. Identifies which system requirements should be allocated to which elements of the system." },
				new EcfCompetenceLevel {Id = 41, CompetenceId = @"C1 ", Level = 1, Description = @"Interacts with users, applies basic product knowledge to respond to user requests. Solves incidents, following prescribed procedures." },
				new EcfCompetenceLevel {Id = 42, CompetenceId = @"C1 ", Level = 2, Description = @"Systematically interprets user problems and identifies solutions and possible side effects. Uses experience to address user problems and interrogates database for potential solutions. Escalates complex or unresolved incidents. Records and tracks issues from outset to conclusion." },
				new EcfCompetenceLevel {Id = 43, CompetenceId = @"C1 ", Level = 3, Description = @"Manages the support process and accountable for agreed SLA. Plans resource allocation to meet defined service level. Acts creatively, and applies continuous service improvement. Manages the support function budget." },
				new EcfCompetenceLevel {Id = 44, CompetenceId = @"C2 ", Level = 2, Description = @"During change, acts systematically to respond to day by day operational needs and react to them, avoiding service disruptions and maintaining coherence to (SLA) and information security requirements." },
				new EcfCompetenceLevel {Id = 45, CompetenceId = @"C2 ", Level = 3, Description = @"Ensures the integrity of the system by controlling the application of functional updates, software or hardware additions and maintenance activities.Complies with budget requirements." },
				new EcfCompetenceLevel {Id = 46, CompetenceId = @"C3 ", Level = 1, Description = @"Acts under guidance to record and track reliability data." },
				new EcfCompetenceLevel {Id = 47, CompetenceId = @"C3 ", Level = 2, Description = @"Systematically analyses performance data and communicates findings to senior experts.Escalates potential service level failures and security risks, recommends actions to improve service reliability.Tracks reliability data against SLA." },
				new EcfCompetenceLevel {Id = 48, CompetenceId = @"C3 ", Level = 3, Description = @"Programmes the schedule of operational tasks.Manages costs and budget according to the internal procedures and external constraints.Identifies the optimum number of people required to resource the operational management of the IS infrastructure." },
				new EcfCompetenceLevel {Id = 49, CompetenceId = @"C4 ", Level = 2, Description = @"Identifies and classifies incident types and service interruptions. Records incidents cataloguing them by symptom and resolution." },
				new EcfCompetenceLevel {Id = 50, CompetenceId = @"C4 ", Level = 3, Description = @"Exploits specialist knowledge and in-depth understanding of the ICT infrastructure and problem management process to identify failures and resolve with minimum outage. Makes sound decisions in emotionally charged environments on appropriate action required to minimise business impact. Rapidly identifies failing component, selects alternatives such as repair, replace or reconfigure." },
				new EcfCompetenceLevel {Id = 51, CompetenceId = @"C4 ", Level = 4, Description = @"Provides leadership and is accountable for the entire problem management process. Schedules and ensures well trained human resources, tools, and diagnostic equipment are available to meet emergency incidents. Has depth of expertise to anticipate critical component failure and make provision for recovery with minimum downtime. Constructs escalation processes to ensure that appropriate resources can be applied to each incident." },
				new EcfCompetenceLevel {Id = 52, CompetenceId = @"D1 ", Level = 4, Description = @"Exploits depth of expertise and leverages external standards and best practices." },
				new EcfCompetenceLevel {Id = 53, CompetenceId = @"D1 ", Level = 5, Description = @"Provides strategic leadership to embed information security into the culture of the organisation." },
				new EcfCompetenceLevel {Id = 54, CompetenceId = @"D2 ", Level = 4, Description = @"Exploits wide ranging specialist knowledge to leverage and authorise the application of external standards and best practices." },
				new EcfCompetenceLevel {Id = 55, CompetenceId = @"D2 ", Level = 5, Description = @"Provides strategic leadership to embed ICT quality (i.e. metrics and continuous improvement) into the culture of the organisation." },
				new EcfCompetenceLevel {Id = 56, CompetenceId = @"D3 ", Level = 2, Description = @"Organises the identification of training needs; collates organisation requirements, identifies, selects and prepares schedule of training interventions." },
				new EcfCompetenceLevel {Id = 57, CompetenceId = @"D3 ", Level = 3, Description = @"Acts creatively to analyse skills gaps; elaborates specific requirements and identifies potential sources for training provision. Has specialist knowledge of the training market and establishes a feedback mechanism to assess the added value of alternative training programmes." },
				new EcfCompetenceLevel {Id = 58, CompetenceId = @"D4 ", Level = 2, Description = @"Understands and applies the principles of the procurement process; places orders based on existing supplier contracts. Ensures the correct execution of orders, including validation of deliverables and correlation with subsequent payments." },
				new EcfCompetenceLevel {Id = 59, CompetenceId = @"D4 ", Level = 3, Description = @"Exploits specialist knowledge to deploy the purchasing process, ensuring positive commercial relationships with suppliers. Selects suppliers, products and services by evaluating performance, cost, timeliness and quality. Decides contract placement and complies with organisational policies." },
				new EcfCompetenceLevel {Id = 60, CompetenceId = @"D4 ", Level = 4, Description = @"Provides leadership for the application of the organisation’s procurement policies and makes recommendations for process enhancement. Applies experience and procurement practice expertise to make ultimate purchasing decisions." },
				new EcfCompetenceLevel {Id = 61, CompetenceId = @"D5 ", Level = 2, Description = @"Organises collaboration between relevant internal departments, for example, technical, sales and legal. Facilitates comparison between customer requirement and available ‘off the shelf’ solutions." },
				new EcfCompetenceLevel {Id = 62, CompetenceId = @"D5 ", Level = 3, Description = @"Acts creatively to develop proposal incorporating a complex solution. Customises solution in a complex technical and legal environment and ensures feasibility, legal and technical validity of customer offer." },
				new EcfCompetenceLevel {Id = 63, CompetenceId = @"D6 ", Level = 3, Description = @"Acts creatively to influence the establishment of a VAR network. Manages the identification and assessment of potential VAR members and sets up support procedures. VARs managed to maximise business performance." },
				new EcfCompetenceLevel {Id = 64, CompetenceId = @"D6 ", Level = 4, Description = @"Exploits wide ranging skills in marketing and sales to create the organisation’s VAR strategy. Establishes the processes by which VARs will be managed to maximise business performance." },
				new EcfCompetenceLevel {Id = 65, CompetenceId = @"D7 ", Level = 3, Description = @"Contributes to the sales process by effectively presenting products or services to customers." },
				new EcfCompetenceLevel {Id = 66, CompetenceId = @"D7 ", Level = 4, Description = @"Assesses and estimates appropriate sales strategies to deliver company results. Decides and allocates annual sales targets and adjusts incentives to meet market conditions." },
				new EcfCompetenceLevel {Id = 67, CompetenceId = @"D7 ", Level = 5, Description = @"Assumes ultimate responsibility for the sales performance of the organisation. Authorises resource allocation, prioritises product and service promotions, advises board directors of sales performance." },
				new EcfCompetenceLevel {Id = 68, CompetenceId = @"D8 ", Level = 2, Description = @"Acts systematically to monitor contract compliance and promptly escalate defaults." },
				new EcfCompetenceLevel {Id = 69, CompetenceId = @"D8 ", Level = 3, Description = @"Evaluates contract performance by monitoring performance indicators. Assures performance of the complete supply chain. Influences the terms of contract renewal." },
				new EcfCompetenceLevel {Id = 70, CompetenceId = @"D8 ", Level = 4, Description = @"Provides leadership for contract compliance and is the final escalation point for issue resolution." },
				new EcfCompetenceLevel {Id = 71, CompetenceId = @"D9 ", Level = 2, Description = @"Briefs/ trains individuals and groups, holds courses of instruction." },
				new EcfCompetenceLevel {Id = 72, CompetenceId = @"D9 ", Level = 3, Description = @"Monitors and addressees the development needs of individuals and teams." },
				new EcfCompetenceLevel {Id = 73, CompetenceId = @"D9 ", Level = 4, Description = @"Takes proactive action and develops organisational processes to address the development needs of individuals, teams and the entire workforce." },
				new EcfCompetenceLevel {Id = 74, CompetenceId = @"D10", Level = 3, Description = @"Analyses business processes and associated information requirements and provides the most appropriate information structure." },
				new EcfCompetenceLevel {Id = 75, CompetenceId = @"D10", Level = 4, Description = @"Integrates the appropriate information structure into the corporate environment." },
				new EcfCompetenceLevel {Id = 76, CompetenceId = @"D10", Level = 5, Description = @"Correlates information and knowledge to create value for the business. Applies innovative solutions based on information retrieved." },
				new EcfCompetenceLevel {Id = 77, CompetenceId = @"D11", Level = 3, Description = @"Establishes reliable relationships with customers and helps them clarify their needs." },
				new EcfCompetenceLevel {Id = 78, CompetenceId = @"D11", Level = 4, Description = @"Exploits wide ranging specialist knowledge of the customers business to offer possible solutions to business needs. Provides expert guidance to the customer by proposing solutions and supplier." },
				new EcfCompetenceLevel {Id = 79, CompetenceId = @"D11", Level = 5, Description = @"Provides leadership in support of the customers’ strategic decisions. Helps customer to envisage new ICT solutions, fosters partnerships and creates value propositions." },
				new EcfCompetenceLevel {Id = 80, CompetenceId = @"D12", Level = 2, Description = @"Understands and applies digital marketing tactics to develop an integrated and effective digital marketing plan using different digital marketing areas such as search, display, e-mail, social media and mobile marketing." },
				new EcfCompetenceLevel {Id = 81, CompetenceId = @"D12", Level = 3, Description = @"Exploits specialist knowledge to utilise analytical tools and assess the effectiveness of websites in terms of technical performance and download speed. Evaluates the user engagement by the application of a wide range of analytical reports. Knows the legal implications of the approaches adopted." },
				new EcfCompetenceLevel {Id = 82, CompetenceId = @"D12", Level = 4, Description = @"Develops clear meaningful objectives for the Digital Marketing Plan. Selects appropriate tools and sets budget targets for the channels adopted. Monitors, analyses and enhances the digital marketing activities in an ongoing manner." },
				new EcfCompetenceLevel {Id = 83, CompetenceId = @"E1 ", Level = 3, Description = @"Exploits skills to provide short-term forecast using market inputs and assessing the organisation’s production and selling capabilities." },
				new EcfCompetenceLevel {Id = 84, CompetenceId = @"E1 ", Level = 4, Description = @"Acts with wide ranging accountability for the production of a long-term forecast. Understands the global marketplace, identifying and evaluating relevant inputs from the broader business, political and social context." },
				new EcfCompetenceLevel {Id = 85, CompetenceId = @"E2 ", Level = 2, Description = @"Understands and applies the principles of project management and applies methodologies, tools and processes to manage simple projects, Optimises costs and minimises waste." },
				new EcfCompetenceLevel {Id = 86, CompetenceId = @"E2 ", Level = 3, Description = @"Accounts for own and others activities, working within the project boundary, making choices and giving instructions, optimising activities and resources. Manages and supervises relationships within the team; plans and establishes team objectives and outputs and documents results." },
				new EcfCompetenceLevel {Id = 87, CompetenceId = @"E2 ", Level = 4, Description = @"Manages complex projects or programmes, including interaction with others. Influences project strategy by proposing new or alternative solutions and balancing effectiveness and efficiency. Is empowered to revise rules and choose standards. Takes overall responsibility for project outcomes, including finance and resource management and works beyond project boundary." },
				new EcfCompetenceLevel {Id = 88, CompetenceId = @"E2 ", Level = 5, Description = @"Provides strategic leadership for extensive interrelated programmes of work to ensure that Information Technology is a change enabling agent and delivers benefit in line with overall business strategic aims. Applies extensive business and technological mastery to conceive and bring innovative ideas to fruition." },
				new EcfCompetenceLevel {Id = 89, CompetenceId = @"E3 ", Level = 2, Description = @"Understands and applies the principles of risk management and investigates ICT solutions to mitigate identified risks." },
				new EcfCompetenceLevel {Id = 90, CompetenceId = @"E3 ", Level = 3, Description = @"Decides on appropriate actions required to adapt security and address risk exposure. Evaluates, manages and ensures validation of exceptions; audits ICT processes and environment." },
				new EcfCompetenceLevel {Id = 91, CompetenceId = @"E3 ", Level = 4, Description = @"Provides leadership to define and make applicable a policy for risk management by considering all the possible constraints, including technical, economic and political issues. Delegates assignments." },
				new EcfCompetenceLevel {Id = 92, CompetenceId = @"E4 ", Level = 3, Description = @"Accounts for own and others actions in managing a limited number of stakeholders." },
				new EcfCompetenceLevel {Id = 93, CompetenceId = @"E4 ", Level = 4, Description = @"Provides leadership for large or many stakeholder relationships. Authorises investment in new and existing relationships. Leads the design of a workable procedure for maintaining positive business relationships." },
				new EcfCompetenceLevel {Id = 94, CompetenceId = @"E5 ", Level = 3, Description = @"Exploits specialist knowledge to research existing ICT processes and solutions in order to define possible innovations. Makes recommendations based on reasoned arguments." },
				new EcfCompetenceLevel {Id = 95, CompetenceId = @"E5 ", Level = 4, Description = @"Provides leadership and authorises implementation of innovations and improvements that will enhance competitiveness or efficiency. Demonstrates to senior management the business advantage of potential changes." },
				new EcfCompetenceLevel {Id = 96, CompetenceId = @"E6 ", Level = 2, Description = @"Communicates and monitors application of the organisation’s quality policy." },
				new EcfCompetenceLevel {Id = 97, CompetenceId = @"E6 ", Level = 3, Description = @"Evaluates quality management indicators and processes based on ICT quality policy and proposes remedial action." },
				new EcfCompetenceLevel {Id = 98, CompetenceId = @"E6 ", Level = 4, Description = @"Assesses and estimates the degree to which quality requirements have been met and provides leadership for quality policy implementation. Provides cross functional leadership for setting and exceeding quality standards." },
				new EcfCompetenceLevel {Id = 99, CompetenceId = @"E7 ", Level = 3, Description = @"Evaluates change requirements and exploits specialist skills to identify possible methods and standards that can be deployed." },
				new EcfCompetenceLevel {Id = 100, CompetenceId = @"E7 ", Level = 4, Description = @"Provides leadership to plan, manage and implement significant ICT led business change." },
				new EcfCompetenceLevel {Id = 101, CompetenceId = @"E7 ", Level = 5, Description = @"Applies pervasive influence to embed organisational change." },
				new EcfCompetenceLevel {Id = 102, CompetenceId = @"E8 ", Level = 2, Description = @"Systematically scans the environment to identify and define vulnerabilities and threats. Records and escalates noncompliance." },
				new EcfCompetenceLevel {Id = 103, CompetenceId = @"E8 ", Level = 3, Description = @"Evaluates security management measures and indicators and decides if compliant to information security policy. Investigates and instigates remedial measures to address any security breaches." },
				new EcfCompetenceLevel {Id = 104, CompetenceId = @"E8 ", Level = 4, Description = @"Provides leadership for the integrity, confidentiality and availability of data stored on information systems and complies with all legal requirements." },
				new EcfCompetenceLevel {Id = 105, CompetenceId = @"E9 ", Level = 4, Description = @"Provides leadership for IS governance strategy by communicating, propagating and controlling relevant processes across the entire ICT infrastructure." },
				new EcfCompetenceLevel {Id = 106, CompetenceId = @"E9 ", Level = 5, Description = @"Defines and aligns the IS governance strategy incorporating it into the organisation’s corporate governance strategy. Adapts the IS governance strategy to take into account new significant events arising from legal, economic, political, business, technological or environmental issues." },

            };
            context.EcfCompetenceLevel.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfCompetenceLevel] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
