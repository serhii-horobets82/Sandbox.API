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
    
		public static bool SeedEmpCompetence(EvoflareDbContext context)
        {
            if (context.EmpCompetence.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(false && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmpCompetence] ON");
			}
            var items = new[]
            {
				new EmpCompetence {Id = 1, CompetenceAreaId = 1, Name = @"IS and Business Strategy Alignment", Summary = @"Anticipates long term business requirements, influences improvement of organisational process efficiency and effectivenes. Determines the IS model and the enterprise architecture in line with the organisation’s policy and ensures a secure environment. Makes strategic IS policy decisions for the enterprise, including sourcing strategies.  " },
				new EmpCompetence {Id = 2, CompetenceAreaId = 1, Name = @"Service Level Management", Summary = @"Defines, validates and makes applicable service level agreements (SLAs) and underpinning contracts for services offered. Negotiates service performance levels taking into account the needs and capacity of stakeholders and business." },
				new EmpCompetence {Id = 3, CompetenceAreaId = 1, Name = @"Business Plan Development", Summary = @"Addresses the design and structure of a business or product plan including the identification of alternative approaches as well as return on investment propositions. Considers the possible and applicable sourcing models. Presents cost benefit analysis and reasoned arguments in support of the selected strategy. Ensures compliance with business and technology strategies. Communicates and sells business plan to relevant stakeholders and addresses political, financial, and organisational interests." },
				new EmpCompetence {Id = 4, CompetenceAreaId = 1, Name = @"Product / Service Planning", Summary = @"Analyses and defines current and target status. Estimates cost effectiveness, points of risk, opportunities, strengths and weaknesses, with a critical approach. Creates structured plans; establishes time scales and milestones, ensuring optimisation of activities and resources. Manages change requests. Defines delivery quantity and provides an overview of additional documentation requirements. Specifies correct handling of products, including legal issues, in accordance with current regulations." },
				new EmpCompetence {Id = 5, CompetenceAreaId = 1, Name = @"Architecture Design", Summary = @"Specifies, refines, updates and makes available a formal approach to implement solutions, necessary to develop and operate the IS architecture. Identifies change requirements and the components involved: hardware, software, applications, processes, information and technology platform. Takes into account interoperability, scalability, usability and security. Maintains alignment between business evolution and technology developments." },
				new EmpCompetence {Id = 6, CompetenceAreaId = 1, Name = @"Application Design", Summary = @"Analyses, specifies, updates and makes available a model to implement applications in accordance with IS policy and user / customer needs. Selects appropriate technical options for application design, optimising the balance between cost and quality. Designs data structures and builds system structure models according to analysis results through modelling languages. Ensures that all aspects take account of interoperability, usability and security. Identifies a common reference framework to validate the models with representative users, based upon development models (e.g. iterative approach)." },
				new EmpCompetence {Id = 7, CompetenceAreaId = 1, Name = @"Technology Trend Monitoring", Summary = @"Investigates latest ICT technological developments to establish understanding of evolving technologies. Devises innovative solutions for integration of new technology into existing products, applications or services or for the creation of new solutions. " },
				new EmpCompetence {Id = 8, CompetenceAreaId = 1, Name = @"Sustainable Development", Summary = @"Estimates the impact of ICT solutions in terms of eco responsibilities including energy consumption. Advises business and ICT stakeholders on sustainable alternatives that are consistent with the business strategy. Applies an ICT purchasing and sales policy which fulfills eco-responsibilities. " },
				new EmpCompetence {Id = 9, CompetenceAreaId = 1, Name = @"Innovating", Summary = @"Devises creative solutions for the provision of new concepts, ideas, products or services. Deploys novel and open thinking to envision exploitation of technological advances to address business / society needs or research direction." },
				new EmpCompetence {Id = 10, CompetenceAreaId = 1, Name = @"Application Development", Summary = @"Interprets the application design to develop a suitable application in accordance with customer needs. Adapts existing solutions by e.g. porting an application to another operating system. Codes, debugs, tests and documents and communicates product development stages. Selects appropriate technical options for development such as reusing, improving or reconfiguration of existing components. Optimises efficiency, cost and quality. Validates results with user representatives, integrates and commissions the overall solution." },
				new EmpCompetence {Id = 11, CompetenceAreaId = 1, Name = @"Component Integration", Summary = @"Integrates hardware, software or sub system components into an existing or a new system. Complies with established processes and procedures such as, configuration management and package maintenance. Takes into account the compatibility of existing and new modules to ensure system integrity, system interoperability and information security. Verifies and tests system capacity and performance and documentation of successful integration." },
				new EmpCompetence {Id = 12, CompetenceAreaId = 1, Name = @"Testing", Summary = @"Constructs and executes systematic test procedures for ICT systems or customer usability requirements to establish compliance with design specifications. Ensures that new or revised components or systems perform to expectation. Ensures meeting of internal, external, national and international standards; including health and safety, usability, performance, reliability or compatibility. Produces documents and reports to evidence certification requirements. " },
				new EmpCompetence {Id = 13, CompetenceAreaId = 1, Name = @"Solution Deployment", Summary = @"Following predefined general standards of practice carries out planned necessary interventions to implement solution, including installing, upgrading or decommissioning. Configures hardware, software or network to ensure interoperability of system components and debugs any resultant faults or incompatibilities. Engages additional specialist resources if required, such as third party network providers. Formally hands over fully operational solution to user and completes documentation recording all relevant information, including equipment addressees, configuration and performance data." },
				new EmpCompetence {Id = 14, CompetenceAreaId = 1, Name = @"Documentation Production", Summary = @"Produces documents describing products, services, components or applications to establish compliance with relevant documentation requirements. Selects appropriate style and media for presentation materials. Creates templates for document-management systems. Ensures that functions and features are documented in an appropriate way. Ensures that existing documents are valid and up to date." },
				new EmpCompetence {Id = 15, CompetenceAreaId = 1, Name = @"Systems Engineering", Summary = @"Engineers software and / or hardware components to meet solution requirements such as specifications, costs, quality, time, energy efficiency, information security and data protection. Follows a systematic methodology to analyse and build the required components and interfaces. Builds system structure models and conducts system behavior simulation. Performs unit and system tests to ensure requirements are met." },
				new EmpCompetence {Id = 16, CompetenceAreaId = 1, Name = @"User Support", Summary = @"Responds to user requests and issues, recording relevant information. Assures resolution or escalates incidents and optimises system performance in accordance with predefined service level agreements (SLAs). Understands how to monitor solution outcome and resultant customer satisfaction. " },
				new EmpCompetence {Id = 17, CompetenceAreaId = 1, Name = @"Change Support", Summary = @"Implements and guides the evolution of an ICT solution. Ensures efficient control and scheduling of software or hardware modifications to prevent multiple upgrades creating unpredictable outcomes. Minimises service disruption as a consequence of changes and adheres to defined service level agreement (SLA). Ensures consideration and compliance with information security procedures. " },
				new EmpCompetence {Id = 18, CompetenceAreaId = 1, Name = @"Service Delivery", Summary = @"Ensures service delivery in accordance with established service level agreements (SLA‘s). Takes proactive action to ensure stable and secure applications and ICT infrastructure to avoid potential service disruptions, attending to capacity planning and to information security. Updates operational document library and logs all service incidents. Maintains monitoring and management tools (i.e. scripts, procedures). Maintains IS services. Takes proactive measures. " },
				new EmpCompetence {Id = 19, CompetenceAreaId = 1, Name = @"Problem Management", Summary = @"Identifies and resolves the root cause of incidents. Takes a proactive approach to avoidance or identification of root cause of ICT problems. Deploys a knowledge system based on recurrence of common errors. Resolves or escalates incidents. Optimises system or component performance. " },
				new EmpCompetence {Id = 20, CompetenceAreaId = 1, Name = @"Information Security Strategy Development", Summary = @"Defines and makes applicable a formal organisational strategy, scope and culture to maintain safety and security of information from external and internal threats, i.e. digital forensic for corporate investigations or intrusion investigation. Provides the foundation for Information Security Management, including role identification and accountability. Uses defined standards to create objectives for information integrity, availability, and data privacy." },
				new EmpCompetence {Id = 21, CompetenceAreaId = 1, Name = @"Information and Knowledge Management", Summary = @"Identifies and manages structured and unstructured information and considers information distribution policies. Creates information structure to enable exploitation and optimisation of information. Understands appropriate tools to be deployed to create, extract, maintain, renew and propagate business knowledge in order to capitalise from the information asset." },
				new EmpCompetence {Id = 22, CompetenceAreaId = 1, Name = @"Needs Identification", Summary = @"Actively listens to internal / external customers, articulates and clarifies their needs. Manages the relationship with all stakeholders to ensure that the solution is in line with business requirements. Proposes different solutions (e.g. make-or-buy), by performing contextual analysis in support of user centered system design. Advises the customer on appropriate solution choices. Acts as an advocate engaging in the implementation or configuration process of the chosen solution." },
				new EmpCompetence {Id = 23, CompetenceAreaId = 1, Name = @"Digital Marketing", Summary = @"Understands the fundamental principles of digital marketing. Distinguishes between the traditional and digital approaches. Appreciates the range of channels available. Assesses the effectiveness of the various approaches and applies rigorous measurement techniques. Plans a coherent strategy using the most effective means available. Understands the data protection and privacy issues involved in the implementation of the marketing strategy." },
				new EmpCompetence {Id = 24, CompetenceAreaId = 1, Name = @"ICT Quality Strategy Development", Summary = @"Defines, improves and refines a formal strategy to satisfy customer expectations and improve business performance (balance between cost and risks). Identifies critical processes influencing service delivery and product performance for definition in the ICT quality management system. Uses defined standards to formulate objectives for service management, product and process quality. Identifies ICT quality management accountability." },
				new EmpCompetence {Id = 25, CompetenceAreaId = 1, Name = @"Education and Training Provision", Summary = @"Defines and implements ICT training policy to address organisational skill needs and gaps. Structures, organises and schedules training programmes and evaluates training quality through a feedback process and implements continuous improvement. Adapts training plans to address changing demand." },
				new EmpCompetence {Id = 26, CompetenceAreaId = 1, Name = @"Purchasing", Summary = @"Applies a consistent procurement procedure, including deployment of the following sub processes: specification requirements, supplier identification, proposal analysis, evaluation of the energy efficiency and environmental compliance of products, suppliers and their processes, contract negotiation, supplier selection and contract placement. Ensures that the entire purchasing process is fit for purpose, adds business value to the organisation compliant to legal and regulatory requirements." },
				new EmpCompetence {Id = 27, CompetenceAreaId = 1, Name = @"Sales Proposal Development", Summary = @"Develops technical proposals to meet customer solution requirements and provide sales personnel with a competitive bid. Underlines the energy efficiency and environmental impact related to a proposal. Collaborates with colleagues to align the service or product solution with the organisation’s capacity to deliver." },
				new EmpCompetence {Id = 28, CompetenceAreaId = 1, Name = @"Channel Management", Summary = @"Develops the strategy for managing third party sales outlets. Ensures optimum commercial performance of the value-added resellers (VARs) channel through the provision of a coherent business and marketing strategy. Defines the targets for volume, geographic coverage and the industry sector for VAR engagements and structures incentive programmes to achieve complimentary sales results." },
				new EmpCompetence {Id = 29, CompetenceAreaId = 1, Name = @"Sales Management", Summary = @"Drives the achievement of sales results through the establishment of a sales strategy. Demonstrates the added value of the organisation’s products and services to new or existing customers and prospects. Establishes a sales support procedure providing efficient response to sales enquiries, consistent with company strategy and policy. Establishes a systematic approach to the entire sales process, including understanding customer needs, forecasting, prospect evaluation, negotiation tactics and sales closure." },
				new EmpCompetence {Id = 30, CompetenceAreaId = 1, Name = @"Contract Management", Summary = @"Provides and negotiates contract in accordance with organisational processes. Ensures that contract and deliverables are provided on time, meet quality standards, and conform to compliance requirements. Addresses non-compliance, escalates significant issues, drives recovery plans and if necessary amends contracts. Maintains budget integrity. Assesses and addresses supplier compliance to legal, health and safety and security standards. Actively pursues regular supplier communication." },
				new EmpCompetence {Id = 31, CompetenceAreaId = 1, Name = @"Personnel Development", Summary = @"Diagnoses individual and group competence, identifying skill needs and skill gaps. Reviews training and development options and selects appropriate methodology taking into account the individual, project and business requirements. Coaches and/ or mentors individuals and teams to address learning needs." },
				new EmpCompetence {Id = 32, CompetenceAreaId = 1, Name = @"Forecast Development", Summary = @"Interprets market needs and evaluates market acceptance of products or services. Assesses the organisation’s potential to meet future production and quality requirements. Applies relevant metrics to enable accurate decision making in support of production, marketing, sales and distribution functions." },
				new EmpCompetence {Id = 33, CompetenceAreaId = 1, Name = @"Project and Portfolio Management", Summary = @"Implements plans for a programme of change. Plans and directs a single or portfolio of ICT projects to ensure co-ordination and management of interdependencies. Orchestrates projects to develop or implement new, internal or externally defined processes to meet identified business needs. Defines activities, responsibilities, critical milestones, resources, skills needs, interfaces and budget, optimises costs and time utilisation, minimises waste and strives for high quality. Develops contingency plans to address potential implementation issues. Delivers project on time, on budget and in accordance with original requirements. Creates and maintains documents to facilitate monitoring of project progress." },
				new EmpCompetence {Id = 34, CompetenceAreaId = 1, Name = @"Risk Management", Summary = @"Implements the management of risk across information systems through the application of the enterprise defined risk management policy and procedure. Assesses risk to the organisation’s business, including web, cloud and mobile resources. Documents potential risk and containment plans." },
				new EmpCompetence {Id = 35, CompetenceAreaId = 1, Name = @"Relationship Management", Summary = @"Establishes and maintains positive business relationships between stakeholders (internal or external) deploying and complying with organisational processes. Maintains regular communication with customer / partner / supplier, and addresses needs through empathy with their environment and managing supply chain communications. Ensures that stakeholder needs, concerns or complaints are understood and addressed in accordance with organisational policy." },
				new EmpCompetence {Id = 36, CompetenceAreaId = 1, Name = @"Process Improvement", Summary = @"Measures effectiveness of existing ICT processes. Researches and benchmarks ICT process design from a variety of sources. Follows a systematic methodology to evaluate, design and implement process or technology changes for measurable business benefit. Assesses potential adverse consequences of process change." },
				new EmpCompetence {Id = 37, CompetenceAreaId = 1, Name = @"ICT Quality Management", Summary = @"Implements ICT quality policy to maintain and enhance service and product provision. Plans and defines indicators to manage quality with respect to ICT strategy. Reviews quality measures and recommends enhancements to influence continuous quality improvement." },
				new EmpCompetence {Id = 38, CompetenceAreaId = 1, Name = @"Business Change Management", Summary = @"Assesses the implications of new digital solutions. Defines the requirements and quantifies the business benefits. Manages the deployment of change taking into account structural and cultural issues. Maintains business and process continuity throughout change, monitoring the impact, taking any required remedial action and refining approach." },
				new EmpCompetence {Id = 39, CompetenceAreaId = 1, Name = @"Information Security Management", Summary = @"Implements information security policy. Monitors and takes action against intrusion, fraud and security breaches or leaks. Ensures that security risks are analysed and managed with respect to enterprise data and information. Reviews security incidents, makes recommendations for security policy and strategy to ensure continuous improvement of security provision." },
				new EmpCompetence {Id = 40, CompetenceAreaId = 1, Name = @"IS Governance", Summary = @"Defines, deploys and controls the management of information systems in line with business imperatives. Takes into account all internal and external parameters such as legislation and industry standard compliance to influence risk management and resource deployment to achieve balanced business benefit." },

            };
            context.EmpCompetence.AddRange(items);

            context.SaveChanges();

			if(false && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EmpCompetence] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
