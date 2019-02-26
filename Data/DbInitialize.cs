using System;
using System.Data;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Evoflare.API.Data
{
    public static class DbInitializer
    {

        public static void Initialize(BaseAppContext context)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();

            context.Database.EnsureCreated();
            if (!context.AppVersion.Any())
            {
                context.AppVersion.Add(new CoreAppVersion()
                {
                    Name = assemblyName.Name,
                    Version =  assemblyName.Version.ToString(),
                    CreationDate = DateTime.Now
                });
                context.SaveChanges();
            }
        }

        public static void Initialize(TechnicalEvaluationContext context)
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
            if (!Directory.Exists(baseDir)) return;

            //context.Database.Migrate();

            foreach (var file in Directory.GetFiles(baseDir, "*.sql"))
            {
                using (var connection = context.Database.GetDbConnection())
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = File.ReadAllText(file, Encoding.UTF8);
                    command.CommandText = command.CommandText.Replace("CREATE DATABASE", "--");
                    command.CommandText = command.CommandText.Replace("GO\r\n", "\r\n");
                    command.CommandText = command.CommandText.Replace("[TechnicalEvaluation]", $"[{connection.Database}]");
                    command.ExecuteNonQuery();
                    command.Dispose();
                }

                //var sqlContentLines = File.ReadAllLines(file, Encoding.UTF8);

                //context.Database.BeginTransaction();

                ////sqlContent.Replace("[TechnicalEvaluation]", $"[{currentDB}]");

                //var insertStatements  = sqlContentLines.Where(e => 
                //    e.StartsWith("INSERT") ||
                //    e.Contains("IDENTITY_INSERT"));

                //insertStatements.ToList().ForEach(line => context.Database.ExecuteSqlCommand(line));

                //context.Database.CommitTransaction();

            }
            /*
            // Look for any Compenetce
            if (!context.Competence.Any())
            {
                var competencesList = new Competence[] {
                new Competence("A1 ", "IS and Business Strategy Alignment", "Anticipates long term business requirements, influences improvement of organisational process efficiency and effectivenes. Determines the IS model and the enterprise architecture in line with the organisation’s policy and ensures a secure environment. Makes strategic IS policy decisions for the enterprise, including sourcing strategies.  "),
                new Competence("A2 ", "Service Level Management", "Defines, validates and makes applicable service level agreements (SLAs), and underpinning contracts for services offered. Negotiates service performance levels taking into account the needs and capacity of stakeholders and business."),
                new Competence("A3 ", "Business Plan Development", "Addresses the design and structure of a business or product plan including the identification of alternative approaches as well as return on investment propositions. Considers the possible and applicable sourcing models. Presents cost benefit analysis and reasoned arguments in support of the selected strategy. Ensures compliance with business and technology strategies. Communicates and sells business plan to relevant stakeholders and addresses political, financial, and organisational interests."),
                new Competence("A4 ", "Product / Service Planning", "Analyses and defines current and target status. Estimates cost effectiveness, points of risk, opportunities, strengths and weaknesses, with a critical approach. Creates structured plans; establishes time scales and milestones, ensuring optimisation of activities and resources. Manages change requests. Defines delivery quantity and provides an overview of additional documentation requirements. Specifies correct handling of products, including legal issues, in accordance with current regulations."),
                new Competence("A5 ", "Architecture Desig", "Specifies, refines, updates and makes available a formal approach to implement solutions, necessary to develop and operate the IS architecture. Identifies change requirements and the components involved: hardware, software, applications, processes, information and technology platform. Takes into account interoperability, scalability, usability and security. Maintains alignment between business evolution and technology developments."),
                new Competence("A6 ", "Application Desig", "Analyses, specifies, updates and makes available a model to implement applications in accordance with IS policy and user / customer needs. Selects appropriate technical options for application design, optimising the balance between cost and quality. Designs data structures and builds system structure models according to analysis results through modelling languages. Ensures that all aspects take account of interoperability, usability and security. Identifies a common reference framework to validate the models with representative users, based upon development models (e.g. iterative approach),."),
                new Competence("A7 ", "Technology Trend Monitoring", "Investigates latest ICT technological developments to establish understanding of evolving technologies. Devises innovative solutions for integration of new technology into existing products, applications or services or for the creation of new solutions. "),
                new Competence("A8 ", "Sustainable Development", "Estimates the impact of ICT solutions in terms of eco responsibilities including energy consumption. Advises business and ICT stakeholders on sustainable alternatives that are consistent with the business strategy. Applies an ICT purchasing and sales policy which fulfills eco-responsibilities. "),
                new Competence("A9 ", "Innovating", "Devises creative solutions for the provision of new concepts, ideas, products or services. Deploys novel and open thinking to envision exploitation of technological advances to address business / society needs or research direction."),
                new Competence("B1 ", "Application Development", "Interprets the application design to develop a suitable application in accordance with customer needs. Adapts existing solutions by e.g. porting an application to another operating system. Codes, debugs, tests and documents and communicates product development stages. Selects appropriate technical options for development such as reusing, improving or reconfiguration of existing components. Optimises efficiency, cost and quality. Validates results with user representatives, integrates and commissions the overall solution."),
                new Competence("B2 ", "Component Integratio", "Integrates hardware, software or sub system components into an existing or a new system. Complies with established processes and procedures such as, configuration management and package maintenance. Takes into account the compatibility of existing and new modules to ensure system integrity, system interoperability and information security. Verifies and tests system capacity and performance and documentation of successful integration."),
                new Competence("B3 ", "Testing", "Constructs and executes systematic test procedures for ICT systems or customer usability requirements to establish compliance with design specifications. Ensures that new or revised components or systems perform to expectation. Ensures meeting of internal, external, national and international standards; including health and safety, usability, performance, reliability or compatibility. Produces documents and reports to evidence certification requirements. "),
                new Competence("B4 ", "Solution Deployment", "Following predefined general standards of practice carries out planned necessary interventions to implement solution, including installing, upgrading or decommissioning. Configures hardware, software or network to ensure interoperability of system components and debugs any resultant faults or incompatibilities. Engages additional specialist resources if required, such as third party network providers. Formally hands over fully operational solution to user and completes documentation recording all relevant information, including equipment addressees, configuration and performance data."),
                new Competence("B5 ", "Documentation Productio", "Produces documents describing products, services, components or applications to establish compliance with relevant documentation requirements. Selects appropriate style and media for presentation materials. Creates templates for document-management systems. Ensures that functions and features are documented in an appropriate way. Ensures that existing documents are valid and up to date."),
                new Competence("B6 ", "Systems Engineering", "Engineers software and / or hardware components to meet solution requirements such as specifications, costs, quality, time, energy efficiency, information security and data protection. Follows a systematic methodology to analyse and build the required components and interfaces. Builds system structure models and conducts system behavior simulation. Performs unit and system tests to ensure requirements are met."),
                new Competence("C1 ", "User Support", "Responds to user requests and issues, recording relevant information. Assures resolution or escalates incidents and optimises system performance in accordance with predefined service level agreements (SLAs),. Understands how to monitor solution outcome and resultant customer satisfaction. "),
                new Competence("C2 ", "Change Support", "Implements and guides the evolution of an ICT solution. Ensures efficient control and scheduling of software or hardware modifications to prevent multiple upgrades creating unpredictable outcomes. Minimises service disruption as a consequence of changes and adheres to defined service level agreement (SLA),. Ensures consideration and compliance with information security procedures. "),
                new Competence("C3 ", "Service Delivery", "Ensures service delivery in accordance with established service level agreements (SLA‘s),. Takes proactive action to ensure stable and secure applications and ICT infrastructure to avoid potential service disruptions, attending to capacity planning and to information security. Updates operational document library and logs all service incidents. Maintains monitoring and management tools (i.e. scripts, procedures),. Maintains IS services. Takes proactive measures. "),
                new Competence("C4 ", "Problem Management", "Identifies and resolves the root cause of incidents. Takes a proactive approach to avoidance or identification of root cause of ICT problems. Deploys a knowledge system based on recurrence of common errors. Resolves or escalates incidents. Optimises system or component performance. "),
                new Competence("D1 ", "Information Security Strategy Development", "Defines and makes applicable a formal organisational strategy, scope and culture to maintain safety and security of information from external and internal threats, i.e. digital forensic for corporate investigations or intrusion investigation. Provides the foundation for Information Security Management, including role identification and accountability. Uses defined standards to create objectives for information integrity, availability, and data privacy."),
                new Competence("D10", "Information and Knowledge Management", "Identifies and manages structured and unstructured information and considers information distribution policies. Creates information structure to enable exploitation and optimisation of information. Understands appropriate tools to be deployed to create, extract, maintain, renew and propagate business knowledge in order to capitalise from the information asset."),
                new Competence("D11", "Needs Identificatio", "Actively listens to internal / external customers, articulates and clarifies their needs. Manages the relationship with all stakeholders to ensure that the solution is in line with business requirements. Proposes different solutions (e.g. make-or-buy),, by performing contextual analysis in support of user centered system design. Advises the customer on appropriate solution choices. Acts as an advocate engaging in the implementation or configuration process of the chosen solution."),
                new Competence("D12", "Digital Marketing", "Understands the fundamental principles of digital marketing. Distinguishes between the traditional and digital approaches. Appreciates the range of channels available. Assesses the effectiveness of the various approaches and applies rigorous measurement techniques. Plans a coherent strategy using the most effective means available. Understands the data protection and privacy issues involved in the implementation of the marketing strategy."),
                new Competence("D2 ", "ICT Quality Strategy Development", "Defines, improves and refines a formal strategy to satisfy customer expectations and improve business performance (balance between cost and risks),. Identifies critical processes influencing service delivery and product performance for definition in the ICT quality management system. Uses defined standards to formulate objectives for service management, product and process quality. Identifies ICT quality management accountability."),
                new Competence("D3 ", "Education and Training Provisio", "Defines and implements ICT training policy to address organisational skill needs and gaps. Structures, organises and schedules training programmes and evaluates training quality through a feedback process and implements continuous improvement. Adapts training plans to address changing demand."),
                new Competence("D4 ", "Purchasing", "Applies a consistent procurement procedure, including deployment of the following sub processes: specification requirements, supplier identification, proposal analysis, evaluation of the energy efficiency and environmental compliance of products, suppliers and their processes, contract negotiation, supplier selection and contract placement. Ensures that the entire purchasing process is fit for purpose, adds business value to the organisation compliant to legal and regulatory requirements."),
                new Competence("D5 ", "Sales Proposal Development", "Develops technical proposals to meet customer solution requirements and provide sales personnel with a competitive bid. Underlines the energy efficiency and environmental impact related to a proposal. Collaborates with colleagues to align the service or product solution with the organisation’s capacity to deliver."),
                new Competence("D6 ", "Channel Management", "Develops the strategy for managing third party sales outlets. Ensures optimum commercial performance of the value-added resellers (VARs), channel through the provision of a coherent business and marketing strategy. Defines the targets for volume, geographic coverage and the industry sector for VAR engagements and structures incentive programmes to achieve complimentary sales results."),
                new Competence("D7 ", "Sales Management", "Drives the achievement of sales results through the establishment of a sales strategy. Demonstrates the added value of the organisation’s products and services to new or existing customers and prospects. Establishes a sales support procedure providing efficient response to sales enquiries, consistent with company strategy and policy. Establishes a systematic approach to the entire sales process, including understanding customer needs, forecasting, prospect evaluation, negotiation tactics and sales closure."),
                new Competence("D8 ", "Contract Management", "Provides and negotiates contract in accordance with organisational processes. Ensures that contract and deliverables are provided on time, meet quality standards, and conform to compliance requirements. Addresses non-compliance, escalates significant issues, drives recovery plans and if necessary amends contracts. Maintains budget integrity. Assesses and addresses supplier compliance to legal, health and safety and security standards. Actively pursues regular supplier communication."),
                new Competence("D9 ", "Personnel Development", "Diagnoses individual and group competence, identifying skill needs and skill gaps. Reviews training and development options and selects appropriate methodology taking into account the individual, project and business requirements. Coaches and/ or mentors individuals and teams to address learning needs."),
                new Competence("E1 ", "Forecast Development", "Interprets market needs and evaluates market acceptance of products or services. Assesses the organisation’s potential to meet future production and quality requirements. Applies relevant metrics to enable accurate decision making in support of production, marketing, sales and distribution functions."),
                new Competence("E2 ", "Project and Portfolio Management", "Implements plans for a programme of change. Plans and directs a single or portfolio of ICT projects to ensure co-ordination and management of interdependencies. Orchestrates projects to develop or implement new, internal or externally defined processes to meet identified business needs. Defines activities, responsibilities, critical milestones, resources, skills needs, interfaces and budget, optimises costs and time utilisation, minimises waste and strives for high quality. Develops contingency plans to address potential implementation issues. Delivers project on time, on budget and in accordance with original requirements. Creates and maintains documents to facilitate monitoring of project progress."),
                new Competence("E3 ", "Risk Management", "Implements the management of risk across information systems through the application of the enterprise defined risk management policy and procedure. Assesses risk to the organisation’s business, including web, cloud and mobile resources. Documents potential risk and containment plans."),
                new Competence("E4 ", "Relationship Management", "Establishes and maintains positive business relationships between stakeholders (internal or external), deploying and complying with organisational processes. Maintains regular communication with customer / partner / supplier, and addresses needs through empathy with their environment and managing supply chain communications. Ensures that stakeholder needs, concerns or complaints are understood and addressed in accordance with organisational policy."),
                new Competence("E5 ", "Process Improvement", "Measures effectiveness of existing ICT processes. Researches and benchmarks ICT process design from a variety of sources. Follows a systematic methodology to evaluate, design and implement process or technology changes for measurable business benefit. Assesses potential adverse consequences of process change."),
                new Competence("E6 ", "ICT Quality Management", "Implements ICT quality policy to maintain and enhance service and product provision. Plans and defines indicators to manage quality with respect to ICT strategy. Reviews quality measures and recommends enhancements to influence continuous quality improvement."),
                new Competence("E7 ", "Business Change Management", "Assesses the implications of new digital solutions. Defines the requirements and quantifies the business benefits. Manages the deployment of change taking into account structural and cultural issues. Maintains business and process continuity throughout change, monitoring the impact, taking any required remedial action and refining approach."),
                new Competence("E8 ", "Information Security Management", "Implements information security policy. Monitors and takes action against intrusion, fraud and security breaches or leaks. Ensures that security risks are analysed and managed with respect to enterprise data and information. Reviews security incidents, makes recommendations for security policy and strategy to ensure continuous improvement of security provision."),
                new Competence("E9 ", "IS Governance", "Defines, deploys and controls the management of information systems in line with business imperatives. Takes into account all internal and external parameters such as legislation and industry standard compliance to influence risk management and resource deployment to achieve balanced business benefit.")
            };
                context.Competence.AddRange(competencesList);
                context.SaveChanges();
            }

            // Look for any CompetenceLevel
            if (!context.CompetenceLevel.Any())
            {
                var competenceLevelList = new CompetenceLevel[] {
                    new CompetenceLevel (1, "A1 ", 4, "Provides leadership for the construction and implementation of long term innovative IS solutions."),
                    new CompetenceLevel (2, "A1 ", 5, "Provides IS strategic leadership to reach consensus and commitment from the management team of the enterprise."),
                    new CompetenceLevel (3, "A2 ", 3, "Ensures the content of the SLA."),
                    new CompetenceLevel (4, "A2 ", 4, "Negotiates revision of SLAs, in accordance with the overall objectives. Ensures the achievement of planned results."),
                    new CompetenceLevel (5, "A3 ", 3, "Exploits specialist knowledge to provide analysis of market environment etc."),
                    new CompetenceLevel (6, "A3 ", 4, "Provides leadership for the creation of an information system strategy that meets the requirements of the business (e.g. distributed, mobility-based), and includes risks and opportunities."),
                    new CompetenceLevel (7, "A3 ", 5, "Applies strategic thinking and organisational leadership to exploit the capability of Information Technology to improve the business."),
                    new CompetenceLevel (8, "A4 ", 2, "Acts systematically to document standard and simple elements of a product."),
                    new CompetenceLevel (9, "A4 ", 3, "Exploits specialist knowledge to create and maintain complex documents."),
                    new CompetenceLevel (10, "A4 ", 4, "Provides leadership and takes responsibility for, developing and maintaining overall plans."),
                    new CompetenceLevel (11, "A5 ", 3, "Exploits specialist knowledge to define relevant ICT technology and specifications to be deployed in the construction of multiple ICT projects, applications or infrastructure improvements."),
                    new CompetenceLevel (12, "A5 ", 4, "Acts with wide ranging accountability to define the strategy to implement ICT technology compliant with business need. Takes account of the current technology platform, obsolescent equipment and latest technological innovations."),
                    new CompetenceLevel (13, "A5 ", 5, "Provides ICT strategic leadership for implementing the enterprise strategy. Applies strategic thinking to discover and recognize new patterns in vast datasets and new ICT systems, to achieve business savings."),
                    new CompetenceLevel (14, "A6 ", 1, "Contributes to the design and general functional specification and interfaces."),
                    new CompetenceLevel (15, "A6 ", 2, "Organises the overall planning of the design of the application."),
                    new CompetenceLevel (16, "A6 ", 3, "Accounts for own and others actions in ensuring that the application is correctly integrated within a complex environment and complies with user / customer needs."),
                    new CompetenceLevel (17, "A7 ", 4, "Exploits wide ranging specialist knowledge of new and emerging technologies, coupled with a deep understanding of the business, to envision and articulate solutions for the future. Provides expert guidance and advice, to the leadership team to support strategic decisionmaking."),
                    new CompetenceLevel (18, "A7 ", 5, "Makes strategic decisions envisioning and articulating future ICT solutions for customer-oriented processes, new business products and services; directs the organisation to build and exploit them."),
                    new CompetenceLevel (19, "A8 ", 3, "Promotes awareness, training and commitment for the deployment of sustainable development and applies the necessary tools for piloting this approach."),
                    new CompetenceLevel (20, "A8 ", 4, "Defines objective and strategy of sustainable IS development in accordance with the organisation’s sustainability policy."),
                    new CompetenceLevel (21, "A9 ", 4, "Applies independent thinking and technology awareness to lead the integration of disparate concepts for the provision of unique solutions."),
                    new CompetenceLevel (22, "A9 ", 5, "Challenges the status quo and provides strategic leadership for the introduction of revolutionary concepts."),
                    new CompetenceLevel (23, "B1 ", 1, "Acts under guidance to develop, test and document applications."),
                    new CompetenceLevel (24, "B1 ", 2, "Systematically develops and validates applications."),
                    new CompetenceLevel (25, "B1 ", 3, "Acts creatively to develop applications and to select appropriate technical options. Accounts for others development activities. Optimizes application development, maintenance and performance by employing design patterns and by reusing proved solutions."),
                    new CompetenceLevel (26, "B2 ", 2, "Acts systematically to identify compatibility of software and hardware specifications. Documents all activities during installation and records deviations and remedial activities."),
                    new CompetenceLevel (27, "B2 ", 3, "Accounts for own and others actions in the integration process. Complies with appropriate standards and change control procedures to maintain integrity of the overall system functionality and reliability."),
                    new CompetenceLevel (28, "B2 ", 4, "Exploits wide ranging specialist knowledge to create a process for the entire integration cycle, including the establishment of internal standards of practice. Provides leadership to marshal and assign resources for programmes of integration."),
                    new CompetenceLevel (29, "B3 ", 1, "Performs simple tests in strict compliance with detailed instructions."),
                    new CompetenceLevel (30, "B3 ", 2, "Organises test programmes and builds scripts to stress test potential vulnerabilities. Records and reports outcomes providing analysis of results."),
                    new CompetenceLevel (31, "B3 ", 3, "Exploits specialist knowledge to supervise complex testing programmes. Ensures tests and results are documented to provide input to subsequent process owners such as designers, users or maintainers. Accountable for compliance with testing procedures including a documented audit trail."),
                    new CompetenceLevel (32, "B3 ", 4, "Exploits wide ranging specialist knowledge to create a process for the entire testing activity, including the establishment of internal standard of practices. Provides expert guidance and advice to the testing team."),
                    new CompetenceLevel (33, "B4 ", 1, "Removes or installs components under guidance and in accordance with detailed instructions."),
                    new CompetenceLevel (34, "B4 ", 2, "Acts systematically to build or deconstruct system elements. Identifies failing components and establishes root cause failures. Provides support to less experienced colleagues."),
                    new CompetenceLevel (35, "B4 ", 3, "Accounts for own and others actions for solution provision and initiates comprehensive communication with stakeholders. Exploits specialist knowledge to influence solution construction providing advice and guidance."),
                    new CompetenceLevel (36, "B5 ", 1, "Uses and applies standards to define document structure."),
                    new CompetenceLevel (37, "B5 ", 2, "Determines documentation requirements taking into account the purpose and environment to which it applies."),
                    new CompetenceLevel (38, "B5 ", 3, "Adapts the level of detail according to the objective of the documentation and the targeted population."),
                    new CompetenceLevel (39, "B6 ", 3, "Ensures interoperability of the system components. Exploits wide ranging specialist knowledge to create a complete system that will satisfy the system constraints and meet the customer’s expectations."),
                    new CompetenceLevel (40, "B6 ", 4, "Handles complexity by developing standard procedures and architectures in support of cohesive product development. Establishes a set of system requirements that will guide the design of the system. Identifies which system requirements should be allocated to which elements of the system."),
                    new CompetenceLevel (41, "C1 ", 1, "Interacts with users, applies basic product knowledge to respond to user requests. Solves incidents, following prescribed procedures."),
                    new CompetenceLevel (42, "C1 ", 2, "Systematically interprets user problems and identifies solutions and possible side effects. Uses experience to address user problems and interrogates database for potential solutions. Escalates complex or unresolved incidents. Records and tracks issues from outset to conclusion."),
                    new CompetenceLevel (43, "C1 ", 3, "Manages the support process and accountable for agreed SLA. Plans resource allocation to meet defined service level. Acts creatively, and applies continuous service improvement. Manages the support function budget."),
                    new CompetenceLevel (44, "C2 ", 2, "During change, acts systematically to respond to day by day operational needs and react to them, avoiding service disruptions and maintaining coherence to (SLA), and information security requirements."),
                    new CompetenceLevel (45, "C2 ", 3, "Ensures the integrity of the system by controlling the application of functional updates, software or hardware additions and maintenance activities.Complies with budget requirements."),
                    new CompetenceLevel (46, "C3 ", 1, "Acts under guidance to record and track reliability data."),
                    new CompetenceLevel (47, "C3 ", 2, "Systematically analyses performance data and communicates findings to senior experts.Escalates potential service level failures and security risks, recommends actions to improve service reliability.Tracks reliability data against SLA."),
                    new CompetenceLevel (48, "C3 ", 3, "Programmes the schedule of operational tasks.Manages costs and budget according to the internal procedures and external constraints.Identifies the optimum number of people required to resource the operational management of the IS infrastructure."),
                    new CompetenceLevel (49, "C4 ", 2, "Identifies and classifies incident types and service interruptions. Records incidents cataloguing them by symptom and resolution."),
                    new CompetenceLevel (50, "C4 ", 3, "Exploits specialist knowledge and in-depth understanding of the ICT infrastructure and problem management process to identify failures and resolve with minimum outage. Makes sound decisions in emotionally charged environments on appropriate action required to minimise business impact. Rapidly identifies failing component, selects alternatives such as repair, replace or reconfigure."),
                    new CompetenceLevel (51, "C4 ", 4, "Provides leadership and is accountable for the entire problem management process. Schedules and ensures well trained human resources, tools, and diagnostic equipment are available to meet emergency incidents. Has depth of expertise to anticipate critical component failure and make provision for recovery with minimum downtime. Constructs escalation processes to ensure that appropriate resources can be applied to each incident."),
                    new CompetenceLevel (52, "D1 ", 4, "Exploits depth of expertise and leverages external standards and best practices."),
                    new CompetenceLevel (53, "D1 ", 5, "Provides strategic leadership to embed information security into the culture of the organisation."),
                    new CompetenceLevel (54, "D2 ", 4, "Exploits wide ranging specialist knowledge to leverage and authorise the application of external standards and best practices."),
                    new CompetenceLevel (55, "D2 ", 5, "Provides strategic leadership to embed ICT quality (i.e. metrics and continuous improvement), into the culture of the organisation."),
                    new CompetenceLevel (56, "D3 ", 2, "Organises the identification of training needs; collates organisation requirements, identifies, selects and prepares schedule of training interventions."),
                    new CompetenceLevel (57, "D3 ", 3, "Acts creatively to analyse skills gaps; elaborates specific requirements and identifies potential sources for training provision. Has specialist knowledge of the training market and establishes a feedback mechanism to assess the added value of alternative training programmes."),
                    new CompetenceLevel (58, "D4 ", 2, "Understands and applies the principles of the procurement process; places orders based on existing supplier contracts. Ensures the correct execution of orders, including validation of deliverables and correlation with subsequent payments."),
                    new CompetenceLevel (59, "D4 ", 3, "Exploits specialist knowledge to deploy the purchasing process, ensuring positive commercial relationships with suppliers. Selects suppliers, products and services by evaluating performance, cost, timeliness and quality. Decides contract placement and complies with organisational policies."),
                    new CompetenceLevel (60, "D4 ", 4, "Provides leadership for the application of the organisation’s procurement policies and makes recommendations for process enhancement. Applies experience and procurement practice expertise to make ultimate purchasing decisions."),
                    new CompetenceLevel (61, "D5 ", 2, "Organises collaboration between relevant internal departments, for example, technical, sales and legal. Facilitates comparison between customer requirement and available ‘off the shelf’ solutions."),
                    new CompetenceLevel (62, "D5 ", 3, "Acts creatively to develop proposal incorporating a complex solution. Customises solution in a complex technical and legal environment and ensures feasibility, legal and technical validity of customer offer."),
                    new CompetenceLevel (63, "D6 ", 3, "Acts creatively to influence the establishment of a VAR network. Manages the identification and assessment of potential VAR members and sets up support procedures. VARs managed to maximise business performance."),
                    new CompetenceLevel (64, "D6 ", 4, "Exploits wide ranging skills in marketing and sales to create the organisation’s VAR strategy. Establishes the processes by which VARs will be managed to maximise business performance."),
                    new CompetenceLevel (65, "D7 ", 3, "Contributes to the sales process by effectively presenting products or services to customers."),
                    new CompetenceLevel (66, "D7 ", 4, "Assesses and estimates appropriate sales strategies to deliver company results. Decides and allocates annual sales targets and adjusts incentives to meet market conditions."),
                    new CompetenceLevel (67, "D7 ", 5, "Assumes ultimate responsibility for the sales performance of the organisation. Authorises resource allocation, prioritises product and service promotions, advises board directors of sales performance."),
                    new CompetenceLevel (68, "D8 ", 2, "Acts systematically to monitor contract compliance and promptly escalate defaults."),
                    new CompetenceLevel (69, "D8 ", 3, "Evaluates contract performance by monitoring performance indicators. Assures performance of the complete supply chain. Influences the terms of contract renewal."),
                    new CompetenceLevel (70, "D8 ", 4, "Provides leadership for contract compliance and is the final escalation point for issue resolution."),
                    new CompetenceLevel (71, "D9 ", 2, "Briefs/ trains individuals and groups, holds courses of instruction."),
                    new CompetenceLevel (72, "D9 ", 3, "Monitors and addressees the development needs of individuals and teams."),
                    new CompetenceLevel (73, "D9 ", 4, "Takes proactive action and develops organisational processes to address the development needs of individuals, teams and the entire workforce."),
                    new CompetenceLevel (74, "D10", 3, "Analyses business processes and associated information requirements and provides the most appropriate information structure."),
                    new CompetenceLevel (75, "D10", 4, "Integrates the appropriate information structure into the corporate environment."),
                    new CompetenceLevel (76, "D10", 5, "Correlates information and knowledge to create value for the business. Applies innovative solutions based on information retrieved."),
                    new CompetenceLevel (77, "D11", 3, "Establishes reliable relationships with customers and helps them clarify their needs."),
                    new CompetenceLevel (78, "D11", 4, "Exploits wide ranging specialist knowledge of the customers business to offer possible solutions to business needs. Provides expert guidance to the customer by proposing solutions and supplier."),
                    new CompetenceLevel (79, "D11", 5, "Provides leadership in support of the customers’ strategic decisions. Helps customer to envisage new ICT solutions, fosters partnerships and creates value propositions."),
                    new CompetenceLevel (80, "D12", 2, "Understands and applies digital marketing tactics to develop an integrated and effective digital marketing plan using different digital marketing areas such as search, display, e-mail, social media and mobile marketing."),
                    new CompetenceLevel (81, "D12", 3, "Exploits specialist knowledge to utilise analytical tools and assess the effectiveness of websites in terms of technical performance and download speed. Evaluates the user engagement by the application of a wide range of analytical reports. Knows the legal implications of the approaches adopted."),
                    new CompetenceLevel (82, "D12", 4, "Develops clear meaningful objectives for the Digital Marketing Plan. Selects appropriate tools and sets budget targets for the channels adopted. Monitors, analyses and enhances the digital marketing activities in an ongoing manner."),
                    new CompetenceLevel (83, "E1 ", 3, "Exploits skills to provide short-term forecast using market inputs and assessing the organisation’s production and selling capabilities."),
                    new CompetenceLevel (84, "E1 ", 4, "Acts with wide ranging accountability for the production of a long-term forecast. Understands the global marketplace, identifying and evaluating relevant inputs from the broader business, political and social context."),
                    new CompetenceLevel (85, "E2 ", 2, "Understands and applies the principles of project management and applies methodologies, tools and processes to manage simple projects, Optimises costs and minimises waste."),
                    new CompetenceLevel (86, "E2 ", 3, "Accounts for own and others activities, working within the project boundary, making choices and giving instructions, optimising activities and resources. Manages and supervises relationships within the team; plans and establishes team objectives and outputs and documents results."),
                    new CompetenceLevel (87, "E2 ", 4, "Manages complex projects or programmes, including interaction with others. Influences project strategy by proposing new or alternative solutions and balancing effectiveness and efficiency. Is empowered to revise rules and choose standards. Takes overall responsibility for project outcomes, including finance and resource management and works beyond project boundary."),
                    new CompetenceLevel (88, "E2 ", 5, "Provides strategic leadership for extensive interrelated programmes of work to ensure that Information Technology is a change enabling agent and delivers benefit in line with overall business strategic aims. Applies extensive business and technological mastery to conceive and bring innovative ideas to fruition."),
                    new CompetenceLevel (89, "E3 ", 2, "Understands and applies the principles of risk management and investigates ICT solutions to mitigate identified risks."),
                    new CompetenceLevel (90, "E3 ", 3, "Decides on appropriate actions required to adapt security and address risk exposure. Evaluates, manages and ensures validation of exceptions; audits ICT processes and environment."),
                    new CompetenceLevel (91, "E3 ", 4, "Provides leadership to define and make applicable a policy for risk management by considering all the possible constraints, including technical, economic and political issues. Delegates assignments."),
                    new CompetenceLevel (92, "E4 ", 3, "Accounts for own and others actions in managing a limited number of stakeholders."),
                    new CompetenceLevel (93, "E4 ", 4, "Provides leadership for large or many stakeholder relationships. Authorises investment in new and existing relationships. Leads the design of a workable procedure for maintaining positive business relationships."),
                    new CompetenceLevel (94, "E5 ", 3, "Exploits specialist knowledge to research existing ICT processes and solutions in order to define possible innovations. Makes recommendations based on reasoned arguments."),
                    new CompetenceLevel (95, "E5 ", 4, "Provides leadership and authorises implementation of innovations and improvements that will enhance competitiveness or efficiency. Demonstrates to senior management the business advantage of potential changes."),
                    new CompetenceLevel (96, "E6 ", 2, "Communicates and monitors application of the organisation’s quality policy."),
                    new CompetenceLevel (97, "E6 ", 3, "Evaluates quality management indicators and processes based on ICT quality policy and proposes remedial action."),
                    new CompetenceLevel (98, "E6 ", 4, "Assesses and estimates the degree to which quality requirements have been met and provides leadership for quality policy implementation. Provides cross functional leadership for setting and exceeding quality standards."),
                    new CompetenceLevel (99, "E7 ", 3, "Evaluates change requirements and exploits specialist skills to identify possible methods and standards that can be deployed."),
                    new CompetenceLevel (100, "E7 ", 4, "Provides leadership to plan, manage and implement significant ICT led business change."),
                    new CompetenceLevel (101, "E7 ", 5, "Applies pervasive influence to embed organisational change."),
                    new CompetenceLevel (102, "E8 ", 2, "Systematically scans the environment to identify and define vulnerabilities and threats. Records and escalates noncompliance."),
                    new CompetenceLevel (103, "E8 ", 3, "Evaluates security management measures and indicators and decides if compliant to information security policy. Investigates and instigates remedial measures to address any security breaches."),
                    new CompetenceLevel (104, "E8 ", 4, "Provides leadership for the integrity, confidentiality and availability of data stored on information systems and complies with all legal requirements."),
                    new CompetenceLevel (105, "E9 ", 4, "Provides leadership for IS governance strategy by communicating, propagating and controlling relevant processes across the entire ICT infrastructure."),
                    new CompetenceLevel (106, "E9 ", 5, "Defines and aligns the IS governance strategy incorporating it into the organisation’s corporate governance strategy. Adapts the IS governance strategy to take into account new significant events arising from legal, economic, political, business, technological or environmental issues.")
                };
                context.CompetenceLevel.AddRange(competenceLevelList);
                context.SaveChanges();
            }

            // Look for any Role
            if (!context.Role.Any())
            {
                var rolesList = new Role[] {
                new Role (1, 6, "Developer", "Designs and/or codes components to meet solution specifications", "Ensures building and implementing of ICT applications. Contributes to lowlevel design. Writes code to ensure optimum efficiency and functionality and user experience."),
                new Role (2, 22, "Technical Specialist", "Maintains and repairs hardware, software and service applications", "To effectively maintain customer hardware/software. Responsible for delivering timely and effective repairs to ensure optimal system performance and superior customer satisfaction."),
                new Role (3, 24, "Solution Designer", "Provides the translation of business requirements into end-to-end IT solutions", "Proposes and designs solutions in line with technical architecture which fit business requirements and support change."),
                new Role (4, 9, "Digital Consultant", "Supports understanding of how digital technologies add value to a business", "Maintains a technology watch to inform stakeholders of existing and emerging technologies and their potential to add business value. Supports the identification of needs and solutions for achieving business and IS strategic goals."),
                new Role (5, 8, "Enterprise Architect", "Designs and maintains the holistic architecture of business processes and Information Systems.", "Maintains a holistic perspective of the organisation strategy, processes, information, security and ICT assets. Links the mission, strategy and business processes to the IT strategy. Ensures project choices are integrated consistently, efficiently and in a sustainable manner according to the enterprise’s digital standards"),
                new Role (6, 21, "Systems Architect", "Plans, designs and integrates ICT system components including hardware, software and services", "Designs, integrates and implements complex technical ICT solutions ensuring procedures and models for development are current and comply with common standards. Monitors new technology developments and applies if appropriate. Provides technological design leadership."),
                new Role (7, 13, "Digital Educator", "Educates and trains Professionals to reach optimal digital competence to support business performance.", "Provide the knowledge and skills required to ensure that people are able to effectively perform tasks in the workplace."),
                new Role (8, 20, "Systems Analyst", "Analyses organisation requirements and specifies software and system requirements for new IT solutions", "Ensures the technical design and contributes to the implementation of new and/or enhanced software provision. Provides solutions for the improvement of organisational efficiency and productivity."),
                new Role (9, 2, "Business Analyst", "Analyses the business domain and optimises business performance through technology applicatio", "Analyses the information and the processes needed to support business plans. Formulates functional and nonfunctional requirements of the business organisation and advises on the lifecycle of the information solutions. Evaluates the impact in terms of change management."),
                new Role (10, 26, "Devops Expect", "Implements processes and tools to successfully deploy DevOps techniques across the entire solution development lifecycle.", "To apply a cross-functional, collaborative approach for the creation ofcustomer-centric software solutions. Introduce automation throughout the software production system to deliver better software faster"),
                new Role (11, 29, "Scrum Master", "Leads and coaches an agile team", "Creates a high performance self-managed dynamic team minimising impediments to development progress. Drives team by applying the agile process to achieve an optimesed work-flow through continuous improvement. Supports team goals and coordinates activities with other teams"),
                new Role (12, 15, "Project Manager", null, null),
                new Role (13, 1, "Account Manager", null, null),
                new Role (14, 3, "Business Information Manager", null, null),
                new Role (15, 4, "Chief Information Officer", null, null),
                new Role (16, 5, "Database Administrator", null, null),
                new Role (17, 7, "Digital Media Specialist", null, null),
                new Role (18, 10, "ICT Operations Manager", null, null),
                new Role (20, 11, "Information Security Manager", null, null),
                new Role (21, 12, "Information Security Specialist", null, null),
                new Role (22, 14, "Network Specialist", null, null),
                new Role (23, 16, "Quality Assurance Manager", null, null),
                new Role (24, 17, "Service Support", null, null),
                new Role (25, 18, "Service Manager", null, null),
                new Role (26, 19, "Systems Administrator", null, null),
                new Role (27, 23, "Test Specialist", null, null),
                new Role (28, 25, "Digital Transformatio", null, null),
                new Role (29, 27, "Data Scientist", null, null),
                new Role (30, 28, "Data Specialist", null, null),
                new Role (31, 30, "Product Owner", null, null)
            };
                context.Role.AddRange(rolesList);
                context.SaveChanges();
            }
            */
        }
    }
}