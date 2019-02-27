USE [master]
GO
/****** Object:  Database [TechnicalEvaluation]    Script Date: 27.02.2019 11:07:36 ******/
CREATE DATABASE [TechnicalEvaluation]
GO
USE [TechnicalEvaluation]
GO
/****** Object:  Table [dbo].[AppVersion]    Script Date: 27.02.2019 11:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppVersion](
	[Name] [nvarchar](450) NOT NULL,
	[Version] [nvarchar](max) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AppVersion] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EcfCompetence]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EcfCompetence](
	[Id] [char](3) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Summary] [nvarchar](max) NULL,
 CONSTRAINT [PK_Competence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EcfCompetenceLevel]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EcfCompetenceLevel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompetenceId] [char](3) NOT NULL,
	[Level] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_CompetenceLevel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EcfEvaluation]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EcfEvaluation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EvaluationId] [int] NOT NULL,
	[Competence] [nchar](3) NOT NULL,
	[CompetenceLevel] [int] NULL,
 CONSTRAINT [PK_EcfEvaluation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EcfRole]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EcfRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Summary] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EcfRoleCompetence]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EcfRoleCompetence](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[CompetenceId] [char](3) NOT NULL,
	[CompetenceLevel] [int] NOT NULL,
 CONSTRAINT [PK_RoleCompetence] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nchar](10) NULL,
	[IsManager] [bit] NOT NULL,
	[EmployeeTypeId] [int] NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[NameTemp] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeEvaluation]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeEvaluation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[ECF] [int] NULL,
	[360Degree] [int] NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployeeEvaluation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePosition]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePosition](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[PositionId] [int] NOT NULL,
	[RelationId] [int] NOT NULL,
	[OrganizationId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeePosition] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeRelations]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeRelations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[ManagerId] [int] NULL,
	[TeamId] [int] NULL,
	[ProjectId] [int] NULL,
	[OrganizationId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeeRelations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeType]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EmployeeType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Position]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PositionRole]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PositionId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PositionRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[OrganizationId] [int] NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 27.02.2019 11:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[OrganizationId] [int] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AppVersion] ([Name], [Version], [CreationDate]) VALUES (N'Evoflare.API', N'0.0.1.1', CAST(N'2019-02-27T10:17:01.4920533' AS DateTime2))
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'A1 ', N'IS and Business Strategy Alignment', N'Anticipates long term business requirements, influences improvement of organisational process efficiency and effectivenes. Determines the IS model and the enterprise architecture in line with the organisation’s policy and ensures a secure environment. Makes strategic IS policy decisions for the enterprise, including sourcing strategies.  ')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'A2 ', N'Service Level Management', N'Defines, validates and makes applicable service level agreements (SLAs) and underpinning contracts for services offered. Negotiates service performance levels taking into account the needs and capacity of stakeholders and business.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'A3 ', N'Business Plan Development', N'Addresses the design and structure of a business or product plan including the identification of alternative approaches as well as return on investment propositions. Considers the possible and applicable sourcing models. Presents cost benefit analysis and reasoned arguments in support of the selected strategy. Ensures compliance with business and technology strategies. Communicates and sells business plan to relevant stakeholders and addresses political, financial, and organisational interests.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'A4 ', N'Product / Service Planning', N'Analyses and defines current and target status. Estimates cost effectiveness, points of risk, opportunities, strengths and weaknesses, with a critical approach. Creates structured plans; establishes time scales and milestones, ensuring optimisation of activities and resources. Manages change requests. Defines delivery quantity and provides an overview of additional documentation requirements. Specifies correct handling of products, including legal issues, in accordance with current regulations.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'A5 ', N'Architecture Design', N'Specifies, refines, updates and makes available a formal approach to implement solutions, necessary to develop and operate the IS architecture. Identifies change requirements and the components involved: hardware, software, applications, processes, information and technology platform. Takes into account interoperability, scalability, usability and security. Maintains alignment between business evolution and technology developments.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'A6 ', N'Application Design', N'Analyses, specifies, updates and makes available a model to implement applications in accordance with IS policy and user / customer needs. Selects appropriate technical options for application design, optimising the balance between cost and quality. Designs data structures and builds system structure models according to analysis results through modelling languages. Ensures that all aspects take account of interoperability, usability and security. Identifies a common reference framework to validate the models with representative users, based upon development models (e.g. iterative approach).')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'A7 ', N'Technology Trend Monitoring', N'Investigates latest ICT technological developments to establish understanding of evolving technologies. Devises innovative solutions for integration of new technology into existing products, applications or services or for the creation of new solutions. ')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'A8 ', N'Sustainable Development', N'Estimates the impact of ICT solutions in terms of eco responsibilities including energy consumption. Advises business and ICT stakeholders on sustainable alternatives that are consistent with the business strategy. Applies an ICT purchasing and sales policy which fulfills eco-responsibilities. ')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'A9 ', N'Innovating', N'Devises creative solutions for the provision of new concepts, ideas, products or services. Deploys novel and open thinking to envision exploitation of technological advances to address business / society needs or research direction.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'B1 ', N'Application Development', N'Interprets the application design to develop a suitable application in accordance with customer needs. Adapts existing solutions by e.g. porting an application to another operating system. Codes, debugs, tests and documents and communicates product development stages. Selects appropriate technical options for development such as reusing, improving or reconfiguration of existing components. Optimises efficiency, cost and quality. Validates results with user representatives, integrates and commissions the overall solution.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'B2 ', N'Component Integration', N'Integrates hardware, software or sub system components into an existing or a new system. Complies with established processes and procedures such as, configuration management and package maintenance. Takes into account the compatibility of existing and new modules to ensure system integrity, system interoperability and information security. Verifies and tests system capacity and performance and documentation of successful integration.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'B3 ', N'Testing', N'Constructs and executes systematic test procedures for ICT systems or customer usability requirements to establish compliance with design specifications. Ensures that new or revised components or systems perform to expectation. Ensures meeting of internal, external, national and international standards; including health and safety, usability, performance, reliability or compatibility. Produces documents and reports to evidence certification requirements. ')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'B4 ', N'Solution Deployment', N'Following predefined general standards of practice carries out planned necessary interventions to implement solution, including installing, upgrading or decommissioning. Configures hardware, software or network to ensure interoperability of system components and debugs any resultant faults or incompatibilities. Engages additional specialist resources if required, such as third party network providers. Formally hands over fully operational solution to user and completes documentation recording all relevant information, including equipment addressees, configuration and performance data.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'B5 ', N'Documentation Production', N'Produces documents describing products, services, components or applications to establish compliance with relevant documentation requirements. Selects appropriate style and media for presentation materials. Creates templates for document-management systems. Ensures that functions and features are documented in an appropriate way. Ensures that existing documents are valid and up to date.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'B6 ', N'Systems Engineering', N'Engineers software and / or hardware components to meet solution requirements such as specifications, costs, quality, time, energy efficiency, information security and data protection. Follows a systematic methodology to analyse and build the required components and interfaces. Builds system structure models and conducts system behavior simulation. Performs unit and system tests to ensure requirements are met.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'C1 ', N'User Support', N'Responds to user requests and issues, recording relevant information. Assures resolution or escalates incidents and optimises system performance in accordance with predefined service level agreements (SLAs). Understands how to monitor solution outcome and resultant customer satisfaction. ')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'C2 ', N'Change Support', N'Implements and guides the evolution of an ICT solution. Ensures efficient control and scheduling of software or hardware modifications to prevent multiple upgrades creating unpredictable outcomes. Minimises service disruption as a consequence of changes and adheres to defined service level agreement (SLA). Ensures consideration and compliance with information security procedures. ')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'C3 ', N'Service Delivery', N'Ensures service delivery in accordance with established service level agreements (SLA‘s). Takes proactive action to ensure stable and secure applications and ICT infrastructure to avoid potential service disruptions, attending to capacity planning and to information security. Updates operational document library and logs all service incidents. Maintains monitoring and management tools (i.e. scripts, procedures). Maintains IS services. Takes proactive measures. ')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'C4 ', N'Problem Management', N'Identifies and resolves the root cause of incidents. Takes a proactive approach to avoidance or identification of root cause of ICT problems. Deploys a knowledge system based on recurrence of common errors. Resolves or escalates incidents. Optimises system or component performance. ')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D1 ', N'Information Security Strategy Development', N'Defines and makes applicable a formal organisational strategy, scope and culture to maintain safety and security of information from external and internal threats, i.e. digital forensic for corporate investigations or intrusion investigation. Provides the foundation for Information Security Management, including role identification and accountability. Uses defined standards to create objectives for information integrity, availability, and data privacy.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D10', N'Information and Knowledge Management', N'Identifies and manages structured and unstructured information and considers information distribution policies. Creates information structure to enable exploitation and optimisation of information. Understands appropriate tools to be deployed to create, extract, maintain, renew and propagate business knowledge in order to capitalise from the information asset.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D11', N'Needs Identification', N'Actively listens to internal / external customers, articulates and clarifies their needs. Manages the relationship with all stakeholders to ensure that the solution is in line with business requirements. Proposes different solutions (e.g. make-or-buy), by performing contextual analysis in support of user centered system design. Advises the customer on appropriate solution choices. Acts as an advocate engaging in the implementation or configuration process of the chosen solution.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D12', N'Digital Marketing', N'Understands the fundamental principles of digital marketing. Distinguishes between the traditional and digital approaches. Appreciates the range of channels available. Assesses the effectiveness of the various approaches and applies rigorous measurement techniques. Plans a coherent strategy using the most effective means available. Understands the data protection and privacy issues involved in the implementation of the marketing strategy.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D2 ', N'ICT Quality Strategy Development', N'Defines, improves and refines a formal strategy to satisfy customer expectations and improve business performance (balance between cost and risks). Identifies critical processes influencing service delivery and product performance for definition in the ICT quality management system. Uses defined standards to formulate objectives for service management, product and process quality. Identifies ICT quality management accountability.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D3 ', N'Education and Training Provision', N'Defines and implements ICT training policy to address organisational skill needs and gaps. Structures, organises and schedules training programmes and evaluates training quality through a feedback process and implements continuous improvement. Adapts training plans to address changing demand.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D4 ', N'Purchasing', N'Applies a consistent procurement procedure, including deployment of the following sub processes: specification requirements, supplier identification, proposal analysis, evaluation of the energy efficiency and environmental compliance of products, suppliers and their processes, contract negotiation, supplier selection and contract placement. Ensures that the entire purchasing process is fit for purpose, adds business value to the organisation compliant to legal and regulatory requirements.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D5 ', N'Sales Proposal Development', N'Develops technical proposals to meet customer solution requirements and provide sales personnel with a competitive bid. Underlines the energy efficiency and environmental impact related to a proposal. Collaborates with colleagues to align the service or product solution with the organisation’s capacity to deliver.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D6 ', N'Channel Management', N'Develops the strategy for managing third party sales outlets. Ensures optimum commercial performance of the value-added resellers (VARs) channel through the provision of a coherent business and marketing strategy. Defines the targets for volume, geographic coverage and the industry sector for VAR engagements and structures incentive programmes to achieve complimentary sales results.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D7 ', N'Sales Management', N'Drives the achievement of sales results through the establishment of a sales strategy. Demonstrates the added value of the organisation’s products and services to new or existing customers and prospects. Establishes a sales support procedure providing efficient response to sales enquiries, consistent with company strategy and policy. Establishes a systematic approach to the entire sales process, including understanding customer needs, forecasting, prospect evaluation, negotiation tactics and sales closure.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D8 ', N'Contract Management', N'Provides and negotiates contract in accordance with organisational processes. Ensures that contract and deliverables are provided on time, meet quality standards, and conform to compliance requirements. Addresses non-compliance, escalates significant issues, drives recovery plans and if necessary amends contracts. Maintains budget integrity. Assesses and addresses supplier compliance to legal, health and safety and security standards. Actively pursues regular supplier communication.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'D9 ', N'Personnel Development', N'Diagnoses individual and group competence, identifying skill needs and skill gaps. Reviews training and development options and selects appropriate methodology taking into account the individual, project and business requirements. Coaches and/ or mentors individuals and teams to address learning needs.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'E1 ', N'Forecast Development', N'Interprets market needs and evaluates market acceptance of products or services. Assesses the organisation’s potential to meet future production and quality requirements. Applies relevant metrics to enable accurate decision making in support of production, marketing, sales and distribution functions.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'E2 ', N'Project and Portfolio Management', N'Implements plans for a programme of change. Plans and directs a single or portfolio of ICT projects to ensure co-ordination and management of interdependencies. Orchestrates projects to develop or implement new, internal or externally defined processes to meet identified business needs. Defines activities, responsibilities, critical milestones, resources, skills needs, interfaces and budget, optimises costs and time utilisation, minimises waste and strives for high quality. Develops contingency plans to address potential implementation issues. Delivers project on time, on budget and in accordance with original requirements. Creates and maintains documents to facilitate monitoring of project progress.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'E3 ', N'Risk Management', N'Implements the management of risk across information systems through the application of the enterprise defined risk management policy and procedure. Assesses risk to the organisation’s business, including web, cloud and mobile resources. Documents potential risk and containment plans.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'E4 ', N'Relationship Management', N'Establishes and maintains positive business relationships between stakeholders (internal or external) deploying and complying with organisational processes. Maintains regular communication with customer / partner / supplier, and addresses needs through empathy with their environment and managing supply chain communications. Ensures that stakeholder needs, concerns or complaints are understood and addressed in accordance with organisational policy.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'E5 ', N'Process Improvement', N'Measures effectiveness of existing ICT processes. Researches and benchmarks ICT process design from a variety of sources. Follows a systematic methodology to evaluate, design and implement process or technology changes for measurable business benefit. Assesses potential adverse consequences of process change.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'E6 ', N'ICT Quality Management', N'Implements ICT quality policy to maintain and enhance service and product provision. Plans and defines indicators to manage quality with respect to ICT strategy. Reviews quality measures and recommends enhancements to influence continuous quality improvement.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'E7 ', N'Business Change Management', N'Assesses the implications of new digital solutions. Defines the requirements and quantifies the business benefits. Manages the deployment of change taking into account structural and cultural issues. Maintains business and process continuity throughout change, monitoring the impact, taking any required remedial action and refining approach.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'E8 ', N'Information Security Management', N'Implements information security policy. Monitors and takes action against intrusion, fraud and security breaches or leaks. Ensures that security risks are analysed and managed with respect to enterprise data and information. Reviews security incidents, makes recommendations for security policy and strategy to ensure continuous improvement of security provision.')
GO
INSERT [dbo].[EcfCompetence] ([Id], [Name], [Summary]) VALUES (N'E9 ', N'IS Governance', N'Defines, deploys and controls the management of information systems in line with business imperatives. Takes into account all internal and external parameters such as legislation and industry standard compliance to influence risk management and resource deployment to achieve balanced business benefit.')
GO
SET IDENTITY_INSERT [dbo].[EcfCompetenceLevel] ON 
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (1, N'A1 ', 4, N'Provides leadership for the construction and implementation of long term innovative IS solutions.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (2, N'A1 ', 5, N'Provides IS strategic leadership to reach consensus and commitment from the management team of the enterprise.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (3, N'A2 ', 3, N'Ensures the content of the SLA.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (4, N'A2 ', 4, N'Negotiates revision of SLAs, in accordance with the overall objectives. Ensures the achievement of planned results.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (5, N'A3 ', 3, N'Exploits specialist knowledge to provide analysis of market environment etc.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (6, N'A3 ', 4, N'Provides leadership for the creation of an information system strategy that meets the requirements of the business (e.g. distributed, mobility-based) and includes risks and opportunities.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (7, N'A3 ', 5, N'Applies strategic thinking and organisational leadership to exploit the capability of Information Technology to improve the business.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (8, N'A4 ', 2, N'Acts systematically to document standard and simple elements of a product.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (9, N'A4 ', 3, N'Exploits specialist knowledge to create and maintain complex documents.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (10, N'A4 ', 4, N'Provides leadership and takes responsibility for, developing and maintaining overall plans.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (11, N'A5 ', 3, N'Exploits specialist knowledge to define relevant ICT technology and specifications to be deployed in the construction of multiple ICT projects, applications or infrastructure improvements.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (12, N'A5 ', 4, N'Acts with wide ranging accountability to define the strategy to implement ICT technology compliant with business need. Takes account of the current technology platform, obsolescent equipment and latest technological innovations.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (13, N'A5 ', 5, N'Provides ICT strategic leadership for implementing the enterprise strategy. Applies strategic thinking to discover and recognize new patterns in vast datasets and new ICT systems, to achieve business savings.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (14, N'A6 ', 1, N'Contributes to the design and general functional specification and interfaces.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (15, N'A6 ', 2, N'Organises the overall planning of the design of the application.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (16, N'A6 ', 3, N'Accounts for own and others actions in ensuring that the application is correctly integrated within a complex environment and complies with user / customer needs.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (17, N'A7 ', 4, N'Exploits wide ranging specialist knowledge of new and emerging technologies, coupled with a deep understanding of the business, to envision and articulate solutions for the future. Provides expert guidance and advice, to the leadership team to support strategic decisionmaking.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (18, N'A7 ', 5, N'Makes strategic decisions envisioning and articulating future ICT solutions for customer-oriented processes, new business products and services; directs the organisation to build and exploit them.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (19, N'A8 ', 3, N'Promotes awareness, training and commitment for the deployment of sustainable development and applies the necessary tools for piloting this approach.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (20, N'A8 ', 4, N'Defines objective and strategy of sustainable IS development in accordance with the organisation’s sustainability policy.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (21, N'A9 ', 4, N'Applies independent thinking and technology awareness to lead the integration of disparate concepts for the provision of unique solutions.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (22, N'A9 ', 5, N'Challenges the status quo and provides strategic leadership for the introduction of revolutionary concepts.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (23, N'B1 ', 1, N'Acts under guidance to develop, test and document applications.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (24, N'B1 ', 2, N'Systematically develops and validates applications.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (25, N'B1 ', 3, N'Acts creatively to develop applications and to select appropriate technical options. Accounts for others development activities. Optimizes application development, maintenance and performance by employing design patterns and by reusing proved solutions.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (26, N'B2 ', 2, N'Acts systematically to identify compatibility of software and hardware specifications. Documents all activities during installation and records deviations and remedial activities.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (27, N'B2 ', 3, N'Accounts for own and others actions in the integration process. Complies with appropriate standards and change control procedures to maintain integrity of the overall system functionality and reliability.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (28, N'B2 ', 4, N'Exploits wide ranging specialist knowledge to create a process for the entire integration cycle, including the establishment of internal standards of practice. Provides leadership to marshal and assign resources for programmes of integration.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (29, N'B3 ', 1, N'Performs simple tests in strict compliance with detailed instructions.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (30, N'B3 ', 2, N'Organises test programmes and builds scripts to stress test potential vulnerabilities. Records and reports outcomes providing analysis of results.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (31, N'B3 ', 3, N'Exploits specialist knowledge to supervise complex testing programmes. Ensures tests and results are documented to provide input to subsequent process owners such as designers, users or maintainers. Accountable for compliance with testing procedures including a documented audit trail.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (32, N'B3 ', 4, N'Exploits wide ranging specialist knowledge to create a process for the entire testing activity, including the establishment of internal standard of practices. Provides expert guidance and advice to the testing team.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (33, N'B4 ', 1, N'Removes or installs components under guidance and in accordance with detailed instructions.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (34, N'B4 ', 2, N'Acts systematically to build or deconstruct system elements. Identifies failing components and establishes root cause failures. Provides support to less experienced colleagues.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (35, N'B4 ', 3, N'Accounts for own and others actions for solution provision and initiates comprehensive communication with stakeholders. Exploits specialist knowledge to influence solution construction providing advice and guidance.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (36, N'B5 ', 1, N'Uses and applies standards to define document structure.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (37, N'B5 ', 2, N'Determines documentation requirements taking into account the purpose and environment to which it applies.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (38, N'B5 ', 3, N'Adapts the level of detail according to the objective of the documentation and the targeted population.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (39, N'B6 ', 3, N'Ensures interoperability of the system components. Exploits wide ranging specialist knowledge to create a complete system that will satisfy the system constraints and meet the customer’s expectations.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (40, N'B6 ', 4, N'Handles complexity by developing standard procedures and architectures in support of cohesive product development. Establishes a set of system requirements that will guide the design of the system. Identifies which system requirements should be allocated to which elements of the system.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (41, N'C1 ', 1, N'Interacts with users, applies basic product knowledge to respond to user requests. Solves incidents, following prescribed procedures.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (42, N'C1 ', 2, N'Systematically interprets user problems and identifies solutions and possible side effects. Uses experience to address user problems and interrogates database for potential solutions. Escalates complex or unresolved incidents. Records and tracks issues from outset to conclusion.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (43, N'C1 ', 3, N'Manages the support process and accountable for agreed SLA. Plans resource allocation to meet defined service level. Acts creatively, and applies continuous service improvement. Manages the support function budget.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (44, N'C2 ', 2, N'During change, acts systematically to respond to day by day operational needs and react to them, avoiding service disruptions and maintaining coherence to (SLA) and information security requirements.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (45, N'C2 ', 3, N'Ensures the integrity of the system by controlling the application of functional updates, software or hardware additions and maintenance activities.Complies with budget requirements.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (46, N'C3 ', 1, N'Acts under guidance to record and track reliability data.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (47, N'C3 ', 2, N'Systematically analyses performance data and communicates findings to senior experts.Escalates potential service level failures and security risks, recommends actions to improve service reliability.Tracks reliability data against SLA.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (48, N'C3 ', 3, N'Programmes the schedule of operational tasks.Manages costs and budget according to the internal procedures and external constraints.Identifies the optimum number of people required to resource the operational management of the IS infrastructure.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (49, N'C4 ', 2, N'Identifies and classifies incident types and service interruptions. Records incidents cataloguing them by symptom and resolution.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (50, N'C4 ', 3, N'Exploits specialist knowledge and in-depth understanding of the ICT infrastructure and problem management process to identify failures and resolve with minimum outage. Makes sound decisions in emotionally charged environments on appropriate action required to minimise business impact. Rapidly identifies failing component, selects alternatives such as repair, replace or reconfigure.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (51, N'C4 ', 4, N'Provides leadership and is accountable for the entire problem management process. Schedules and ensures well trained human resources, tools, and diagnostic equipment are available to meet emergency incidents. Has depth of expertise to anticipate critical component failure and make provision for recovery with minimum downtime. Constructs escalation processes to ensure that appropriate resources can be applied to each incident.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (52, N'D1 ', 4, N'Exploits depth of expertise and leverages external standards and best practices.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (53, N'D1 ', 5, N'Provides strategic leadership to embed information security into the culture of the organisation.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (54, N'D2 ', 4, N'Exploits wide ranging specialist knowledge to leverage and authorise the application of external standards and best practices.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (55, N'D2 ', 5, N'Provides strategic leadership to embed ICT quality (i.e. metrics and continuous improvement) into the culture of the organisation.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (56, N'D3 ', 2, N'Organises the identification of training needs; collates organisation requirements, identifies, selects and prepares schedule of training interventions.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (57, N'D3 ', 3, N'Acts creatively to analyse skills gaps; elaborates specific requirements and identifies potential sources for training provision. Has specialist knowledge of the training market and establishes a feedback mechanism to assess the added value of alternative training programmes.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (58, N'D4 ', 2, N'Understands and applies the principles of the procurement process; places orders based on existing supplier contracts. Ensures the correct execution of orders, including validation of deliverables and correlation with subsequent payments.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (59, N'D4 ', 3, N'Exploits specialist knowledge to deploy the purchasing process, ensuring positive commercial relationships with suppliers. Selects suppliers, products and services by evaluating performance, cost, timeliness and quality. Decides contract placement and complies with organisational policies.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (60, N'D4 ', 4, N'Provides leadership for the application of the organisation’s procurement policies and makes recommendations for process enhancement. Applies experience and procurement practice expertise to make ultimate purchasing decisions.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (61, N'D5 ', 2, N'Organises collaboration between relevant internal departments, for example, technical, sales and legal. Facilitates comparison between customer requirement and available ‘off the shelf’ solutions.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (62, N'D5 ', 3, N'Acts creatively to develop proposal incorporating a complex solution. Customises solution in a complex technical and legal environment and ensures feasibility, legal and technical validity of customer offer.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (63, N'D6 ', 3, N'Acts creatively to influence the establishment of a VAR network. Manages the identification and assessment of potential VAR members and sets up support procedures. VARs managed to maximise business performance.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (64, N'D6 ', 4, N'Exploits wide ranging skills in marketing and sales to create the organisation’s VAR strategy. Establishes the processes by which VARs will be managed to maximise business performance.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (65, N'D7 ', 3, N'Contributes to the sales process by effectively presenting products or services to customers.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (66, N'D7 ', 4, N'Assesses and estimates appropriate sales strategies to deliver company results. Decides and allocates annual sales targets and adjusts incentives to meet market conditions.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (67, N'D7 ', 5, N'Assumes ultimate responsibility for the sales performance of the organisation. Authorises resource allocation, prioritises product and service promotions, advises board directors of sales performance.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (68, N'D8 ', 2, N'Acts systematically to monitor contract compliance and promptly escalate defaults.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (69, N'D8 ', 3, N'Evaluates contract performance by monitoring performance indicators. Assures performance of the complete supply chain. Influences the terms of contract renewal.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (70, N'D8 ', 4, N'Provides leadership for contract compliance and is the final escalation point for issue resolution.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (71, N'D9 ', 2, N'Briefs/ trains individuals and groups, holds courses of instruction.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (72, N'D9 ', 3, N'Monitors and addressees the development needs of individuals and teams.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (73, N'D9 ', 4, N'Takes proactive action and develops organisational processes to address the development needs of individuals, teams and the entire workforce.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (74, N'D10', 3, N'Analyses business processes and associated information requirements and provides the most appropriate information structure.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (75, N'D10', 4, N'Integrates the appropriate information structure into the corporate environment.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (76, N'D10', 5, N'Correlates information and knowledge to create value for the business. Applies innovative solutions based on information retrieved.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (77, N'D11', 3, N'Establishes reliable relationships with customers and helps them clarify their needs.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (78, N'D11', 4, N'Exploits wide ranging specialist knowledge of the customers business to offer possible solutions to business needs. Provides expert guidance to the customer by proposing solutions and supplier.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (79, N'D11', 5, N'Provides leadership in support of the customers’ strategic decisions. Helps customer to envisage new ICT solutions, fosters partnerships and creates value propositions.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (80, N'D12', 2, N'Understands and applies digital marketing tactics to develop an integrated and effective digital marketing plan using different digital marketing areas such as search, display, e-mail, social media and mobile marketing.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (81, N'D12', 3, N'Exploits specialist knowledge to utilise analytical tools and assess the effectiveness of websites in terms of technical performance and download speed. Evaluates the user engagement by the application of a wide range of analytical reports. Knows the legal implications of the approaches adopted.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (82, N'D12', 4, N'Develops clear meaningful objectives for the Digital Marketing Plan. Selects appropriate tools and sets budget targets for the channels adopted. Monitors, analyses and enhances the digital marketing activities in an ongoing manner.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (83, N'E1 ', 3, N'Exploits skills to provide short-term forecast using market inputs and assessing the organisation’s production and selling capabilities.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (84, N'E1 ', 4, N'Acts with wide ranging accountability for the production of a long-term forecast. Understands the global marketplace, identifying and evaluating relevant inputs from the broader business, political and social context.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (85, N'E2 ', 2, N'Understands and applies the principles of project management and applies methodologies, tools and processes to manage simple projects, Optimises costs and minimises waste.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (86, N'E2 ', 3, N'Accounts for own and others activities, working within the project boundary, making choices and giving instructions, optimising activities and resources. Manages and supervises relationships within the team; plans and establishes team objectives and outputs and documents results.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (87, N'E2 ', 4, N'Manages complex projects or programmes, including interaction with others. Influences project strategy by proposing new or alternative solutions and balancing effectiveness and efficiency. Is empowered to revise rules and choose standards. Takes overall responsibility for project outcomes, including finance and resource management and works beyond project boundary.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (88, N'E2 ', 5, N'Provides strategic leadership for extensive interrelated programmes of work to ensure that Information Technology is a change enabling agent and delivers benefit in line with overall business strategic aims. Applies extensive business and technological mastery to conceive and bring innovative ideas to fruition.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (89, N'E3 ', 2, N'Understands and applies the principles of risk management and investigates ICT solutions to mitigate identified risks.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (90, N'E3 ', 3, N'Decides on appropriate actions required to adapt security and address risk exposure. Evaluates, manages and ensures validation of exceptions; audits ICT processes and environment.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (91, N'E3 ', 4, N'Provides leadership to define and make applicable a policy for risk management by considering all the possible constraints, including technical, economic and political issues. Delegates assignments.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (92, N'E4 ', 3, N'Accounts for own and others actions in managing a limited number of stakeholders.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (93, N'E4 ', 4, N'Provides leadership for large or many stakeholder relationships. Authorises investment in new and existing relationships. Leads the design of a workable procedure for maintaining positive business relationships.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (94, N'E5 ', 3, N'Exploits specialist knowledge to research existing ICT processes and solutions in order to define possible innovations. Makes recommendations based on reasoned arguments.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (95, N'E5 ', 4, N'Provides leadership and authorises implementation of innovations and improvements that will enhance competitiveness or efficiency. Demonstrates to senior management the business advantage of potential changes.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (96, N'E6 ', 2, N'Communicates and monitors application of the organisation’s quality policy.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (97, N'E6 ', 3, N'Evaluates quality management indicators and processes based on ICT quality policy and proposes remedial action.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (98, N'E6 ', 4, N'Assesses and estimates the degree to which quality requirements have been met and provides leadership for quality policy implementation. Provides cross functional leadership for setting and exceeding quality standards.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (99, N'E7 ', 3, N'Evaluates change requirements and exploits specialist skills to identify possible methods and standards that can be deployed.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (100, N'E7 ', 4, N'Provides leadership to plan, manage and implement significant ICT led business change.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (101, N'E7 ', 5, N'Applies pervasive influence to embed organisational change.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (102, N'E8 ', 2, N'Systematically scans the environment to identify and define vulnerabilities and threats. Records and escalates noncompliance.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (103, N'E8 ', 3, N'Evaluates security management measures and indicators and decides if compliant to information security policy. Investigates and instigates remedial measures to address any security breaches.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (104, N'E8 ', 4, N'Provides leadership for the integrity, confidentiality and availability of data stored on information systems and complies with all legal requirements.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (105, N'E9 ', 4, N'Provides leadership for IS governance strategy by communicating, propagating and controlling relevant processes across the entire ICT infrastructure.')
GO
INSERT [dbo].[EcfCompetenceLevel] ([Id], [CompetenceId], [Level], [Description]) VALUES (106, N'E9 ', 5, N'Defines and aligns the IS governance strategy incorporating it into the organisation’s corporate governance strategy. Adapts the IS governance strategy to take into account new significant events arising from legal, economic, political, business, technological or environmental issues.')
GO
SET IDENTITY_INSERT [dbo].[EcfCompetenceLevel] OFF
GO
SET IDENTITY_INSERT [dbo].[EcfRole] ON 
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (1, 6, N'Developer', N'Designs and/or codes components to meet solution specifications', N'Ensures building and implementing of ICT applications. Contributes to lowlevel design. Writes code to ensure optimum efficiency and functionality and user experience.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (2, 22, N'Technical Specialist', N'Maintains and repairs hardware, software and service applications', N'To effectively maintain customer hardware/software. Responsible for delivering timely and effective repairs to ensure optimal system performance and superior customer satisfaction.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (3, 24, N'Solution Designer', N'Provides the translation of business requirements into end-to-end IT solutions', N'Proposes and designs solutions in line with technical architecture which fit business requirements and support change.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (4, 9, N'Digital Consultant', N'Supports understanding of how digital technologies add value to a business', N'Maintains a technology watch to inform stakeholders of existing and emerging technologies and their potential to add business value. Supports the identification of needs and solutions for achieving business and IS strategic goals.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (5, 8, N'Enterprise Architect', N'Designs and maintains the holistic architecture of business processes and Information Systems.', N'Maintains a holistic perspective of the organisation strategy, processes, information, security and ICT assets. Links the mission, strategy and business processes to the IT strategy. Ensures project choices are integrated consistently, efficiently and in a sustainable manner according to the enterprise’s digital standards')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (6, 21, N'Systems Architect', N'Plans, designs and integrates ICT system components including hardware, software and services', N'Designs, integrates and implements complex technical ICT solutions ensuring procedures and models for development are current and comply with common standards. Monitors new technology developments and applies if appropriate. Provides technological design leadership.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (7, 13, N'Digital Educator', N'Educates and trains Professionals to reach optimal digital competence to support business performance.', N'Provide the knowledge and skills required to ensure that people are able to effectively perform tasks in the workplace.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (8, 20, N'Systems Analyst', N'Analyses organisation requirements and specifies software and system requirements for new IT solutions', N'Ensures the technical design and contributes to the implementation of new and/or enhanced software provision. Provides solutions for the improvement of organisational efficiency and productivity.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (9, 2, N'Business Analyst', N'Analyses the business domain and optimises business performance through technology application', N'Analyses the information and the processes needed to support business plans. Formulates functional and nonfunctional requirements of the business organisation and advises on the lifecycle of the information solutions. Evaluates the impact in terms of change management.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (10, 26, N'Devops Expect', N'Implements processes and tools to successfully deploy DevOps techniques across the entire solution development lifecycle.', N'To apply a cross-functional, collaborative approach for the creation of customer-centric software solutions. Introduce automation throughout the software production system to deliver better software faster')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (11, 29, N'Scrum Master', N'Leads and coaches an agile team', N'Creates a high performance self-managed dynamic team minimising impediments to development progress. Drives team by applying the agile process to achieve an optimesed work-flow through continuous improvement. Supports team goals and coordinates activities with other teams')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (12, 15, N'Project Manager', N'Manages projects to achieve optimal performance and results.', N'Defines, implements and manages projects from conception to final delivery. Responsible for achieving optimal results, conforming to standards for quality, safety and sustainability and complying with defined scope, performance, costs, and schedule. Deploys agile practices where applicable.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (13, 1, N'Account Manager', N'Senior focal point for client sales and customer satisfaction.', N'Builds business relationships with clients to facilitate the sale of hardware, software, telecommunications or ICT services. Identifies opportunities and manages sourcing and delivery of products to customers. Has responsibility for achieving sales targets and maintaining profitability.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (14, 3, N'Business Information Manager', N'Proposes, plans and manages functional development of the Information System (IS) focusing upon the needs of users.', N'Aligns the Information System to the business strategy within their area/domain. Ensures continuous enhancement whilst accounting for user requirements, service quality and budgetary constraints.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (15, 4, N'Chief Information Officer', N'Develops and maintains Information Systems to generate value for the business and meet the organisation’s needs.', N'Ensures the alignment of the Information Systems strategy with the business strategy. Provides leadership for the implementation and development of the organisations architecture and applications.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (16, 5, N'Database Administrator', N'Designs, implements, or monitors and maintains data sets, structured (databases) and unstructured (big data).', N'Administer and monitor data management systems and ensures design, consistency, quality and security.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (17, 7, N'Digital Media Specialist', N'Integrates digital technology components for internal and external communication purposes.', N'Designs and codes social media applications and websites. Makes recommendations on Application Programming Interface (API) and supports efficiency through appropriate content management systems.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (18, 10, N'ICT Operations Manager', N'Manages operations, people and overall ICT resources.', N'Implements and maintains a designated part of an ICT operation ensuring that activities are conducted in accordance with organisational rules, processes and standards. Plans changes and implements them in accordance with organisational strategy and budget. Risk manages and ensures the effectiveness of the ICT infrastructure.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (20, 11, N'Information Security Manager', N'Leads and manages the organisation information security policy.', N'Defines the information security strategy and manages implementation across the organisation. Embeds proactive information security protection by assessing, informing, alerting and educating the entire organisation.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (21, 12, N'Information Security Specialist', N'Ensures the implementation of the organisation’s information security policy by the secure and appropriate use of ICT resources.', N'Defines, proposes and implements necessary information security technique and practices in compliance with information security standards and procedures. Contributes to security practices, awareness and compliance by providing advice, support, information and training.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (22, 14, N'Network Specialist', N'Ensures the alignment of the network, including telecommunication and/or computer infrastructure to meet the organisation’s communication needs.', N'Manages and operates a networked information system, solving problems and faults to ensure defined service levels. Monitors and improves network performances and security.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (23, 16, N'Quality Assurance Manager', N'Ensures that processes and organisations implementing Information Systems comply to quality policies.', N'Establishes and operates an ICT quality approach aligned with the organisation’s culture. Commits the organisation to the achievement of quality goals and an encourages an environment of continuous improvement.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (24, 17, N'Service Support', N'Provides remote or onsite diagnosis or guidance to internal or external clients with technical issues.', N'To provide user support and troubleshoot ICT problems and issues. The primary objective is to enable users to maximize their productivity through efficient and secure use of ICT equipment or software applications.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (25, 18, N'Service Manager', N'Plans, implements and manages solution provision.', N'Manages the definition of Service Level Agreements (SLAs), Operational Level Agreements (OLAs) contracts and Key Performance Indicators (KPIs). Provides people management of staff monitoring, reporting and fulfilling service activities. Takes mitigation action in case of nonfulfilment of agreements.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (26, 19, N'Systems Administrator', N'Administers ICT System components to meet service requirements.', N'Installs software, configures and upgrades ICT systems. Administers day-to-day operations to satisfy continuity of service, recovery, security and performance needs.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (27, 23, N'Test Specialist', N'Designs and performs testing plans.', N'Ensures delivered or existing products, applications or services comply with technical and user needs and specifications. For existing systems, applications, innovations and changes; diagnoses failure of products or services to meet specification.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (28, 25, N'Digital Transformation', N'Provides leadership for the implementation of the digital transformation strategy of the organisation.', N'Drive cultural change and build digital capability to deliver innovative business models and processes.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (29, 27, N'Data Scientist', N'Leads the process of applying data analytics. Delivers insights from data by optimising the analytics process and presenting visual data representations.', N'Finds, manages and merges multiple data sources and ensures consistency of datasets. Identifies the mathematical models, selects and optimises the algorhythms to deliver business value through insights. Communicates patterns and recommends ways of applying data.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (30, 28, N'Data Specialist', N'Ensures the implementation of the organisations data management policy.', N'Ensures asset protection through the provision of clean, consistent, quality assured data. Maintains the integrity of data, stores and searches data and supports presentation of data analysis.')
GO
INSERT [dbo].[EcfRole] ([Id], [RoleId], [Name], [Summary], [Description]) VALUES (31, 30, N'Product Owner', N'Represents the voice of the customer in an Agile team.', N'Understands customer requirements and validates that the developed software solution meets requirements. Links business and Agile teams.')
GO
SET IDENTITY_INSERT [dbo].[EcfRole] OFF
GO
SET IDENTITY_INSERT [dbo].[EcfRoleCompetence] ON 
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (1, 1, N'D5 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (2, 1, N'D6 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (3, 1, N'D7 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (4, 1, N'E1 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (5, 1, N'E4 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (6, 2, N'A1 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (7, 2, N'A3 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (8, 2, N'D10', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (9, 2, N'D11', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (10, 2, N'E5 ', 1)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (11, 3, N'A1 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (12, 3, N'A3 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (13, 3, N'E4 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (14, 3, N'E7 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (15, 3, N'E9 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (16, 4, N'A1 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (17, 4, N'A3 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (18, 4, N'E2 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (19, 4, N'E4 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (20, 4, N'E9 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (21, 5, N'B1 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (22, 5, N'B2 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (23, 5, N'C2 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (24, 5, N'D10', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (25, 5, N'E8 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (26, 6, N'B1 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (27, 6, N'B2 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (28, 6, N'B3 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (29, 6, N'B5 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (30, 6, N'C4 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (31, 7, N'A6 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (32, 7, N'B1 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (33, 7, N'B3 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (34, 7, N'B4 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (35, 7, N'D12', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (36, 8, N'A1 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (37, 8, N'A3 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (38, 8, N'A5 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (39, 8, N'A7 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (40, 8, N'E8 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (41, 9, N'A7 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (42, 9, N'A9 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (43, 9, N'D11', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (44, 9, N'E3 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (45, 9, N'E7 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (46, 10, N'D9 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (47, 10, N'E2 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (48, 10, N'E3 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (49, 10, N'E6 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (50, 10, N'E8 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (51, 11, N'A7 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (52, 11, N'D1 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (53, 11, N'E3 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (54, 11, N'E8 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (55, 11, N'E9 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (56, 12, N'A7 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (57, 12, N'A9 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (58, 12, N'D1 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (59, 12, N'D3 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (60, 12, N'E3 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (61, 13, N'B5 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (62, 13, N'D3 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (63, 13, N'D9 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (64, 13, N'E2 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (65, 14, N'A6 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (66, 14, N'B2 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (67, 14, N'B4 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (68, 14, N'C4 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (69, 14, N'E8 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (70, 15, N'A4 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (71, 15, N'E2 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (72, 15, N'E3 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (73, 15, N'E4 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (74, 15, N'E7 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (75, 16, N'D2 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (76, 16, N'E3 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (77, 16, N'E5 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (78, 16, N'E6 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (79, 17, N'C1 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (80, 17, N'C2 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (81, 17, N'C3 ', 1)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (82, 17, N'C4 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (83, 18, N'A2 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (84, 18, N'C3 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (85, 18, N'C4 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (86, 18, N'D8 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (87, 18, N'D9 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (88, 19, N'B2 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (89, 19, N'B3 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (90, 19, N'C2 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (91, 19, N'C4 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (92, 19, N'E8 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (93, 20, N'A5 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (94, 20, N'B5 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (95, 20, N'B6 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (96, 20, N'E5 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (97, 21, N'A5 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (98, 21, N'A7 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (99, 21, N'A9 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (100, 21, N'B2 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (101, 21, N'B6 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (102, 22, N'C2 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (103, 22, N'C3 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (104, 22, N'C4 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (105, 22, N'E3 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (106, 22, N'E6 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (107, 23, N'B2 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (109, 23, N'B3 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (110, 23, N'B4 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (111, 23, N'B5 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (112, 23, N'E3 ', 2)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (113, 24, N'A6 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (114, 24, N'A9 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (115, 24, N'D10', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (116, 24, N'D11', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (117, 25, N'A3 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (118, 25, N'A5 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (119, 25, N'A9 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (120, 25, N'E7 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (121, 25, N'E9 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (122, 26, N'B1 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (123, 26, N'B2 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (124, 26, N'B3 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (125, 26, N'B4 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (126, 26, N'C2 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (127, 27, N'A7 ', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (128, 27, N'A9 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (129, 27, N'D10', 5)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (130, 27, N'D11', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (131, 27, N'E1 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (132, 28, N'A6 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (133, 28, N'D10', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (134, 28, N'E6 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (135, 28, N'E8 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (136, 29, N'B3 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (137, 29, N'B6 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (138, 29, N'D9 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (139, 29, N'E4 ', 3)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (140, 30, N'A4 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (141, 30, N'A9 ', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (142, 30, N'D11', 4)
GO
INSERT [dbo].[EcfRoleCompetence] ([Id], [RoleId], [CompetenceId], [CompetenceLevel]) VALUES (143, 30, N'E4 ', 4)
GO
SET IDENTITY_INSERT [dbo].[EcfRoleCompetence] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (1, NULL, 1, 1, 1, N'John Manager')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (2, NULL, 1, 1, 1, N'Bob Manager')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (3, NULL, 0, 3, 1, N'Karl QA')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (4, NULL, 0, 4, 1, N'Marta AutoQA')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (6, NULL, 0, 2, 1, N'Linus Developer')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (7, NULL, 0, 2, 1, N'Mark Developer')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (10, NULL, 1, 1, 1, N'Petra Manager')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (11, NULL, 1, 1, 1, N'Barak MEGA Manager')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (12, NULL, 0, 3, 1, N'Tapak QA')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (13, NULL, 0, 3, 1, N'Mikki QA')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (15, NULL, 0, 4, 1, N'Billy AutoQA')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (16, NULL, 0, 2, 1, N'Todd Developer')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (17, NULL, 0, 2, 1, N'Riana Developer')
GO
INSERT [dbo].[Employee] ([Id], [UserId], [IsManager], [EmployeeTypeId], [OrganizationId], [NameTemp]) VALUES (18, NULL, 0, 2, 1, N'Mila Developer')
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeRelations] ON 
GO
INSERT [dbo].[EmployeeRelations] ([Id], [EmployeeId], [ManagerId], [TeamId], [ProjectId], [OrganizationId]) VALUES (1, NULL, 1, 1, 1, 1)
GO
INSERT [dbo].[EmployeeRelations] ([Id], [EmployeeId], [ManagerId], [TeamId], [ProjectId], [OrganizationId]) VALUES (2, 6, NULL, 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[EmployeeRelations] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeType] ON 
GO
INSERT [dbo].[EmployeeType] ([Id], [Type]) VALUES (1, N'MANAGER')
GO
INSERT [dbo].[EmployeeType] ([Id], [Type]) VALUES (2, N'DEV')
GO
INSERT [dbo].[EmployeeType] ([Id], [Type]) VALUES (3, N'QA')
GO
INSERT [dbo].[EmployeeType] ([Id], [Type]) VALUES (4, N'AUTOMATION')
GO
INSERT [dbo].[EmployeeType] ([Id], [Type]) VALUES (5, N'DEVOPS')
GO
INSERT [dbo].[EmployeeType] ([Id], [Type]) VALUES (6, N'OPERATIONS')
GO
SET IDENTITY_INSERT [dbo].[EmployeeType] OFF
GO
SET IDENTITY_INSERT [dbo].[Organization] ON 
GO
INSERT [dbo].[Organization] ([Id], [Name]) VALUES (1, N'Smart CORP')
GO
SET IDENTITY_INSERT [dbo].[Organization] OFF
GO
SET IDENTITY_INSERT [dbo].[Position] ON 
GO
INSERT [dbo].[Position] ([Id], [Name], [OrganizationId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDeleted]) VALUES (1, N'First position', 0, CAST(N'2019-02-26T18:14:57.683' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[Position] ([Id], [Name], [OrganizationId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDeleted]) VALUES (2, N'Developer 1', 0, CAST(N'2019-02-26T19:18:03.260' AS DateTime), 1, NULL, NULL, 0)
GO
INSERT [dbo].[Position] ([Id], [Name], [OrganizationId], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [IsDeleted]) VALUES (3, N'Developer 2', 0, CAST(N'2019-02-26T19:19:59.907' AS DateTime), 1, NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[Position] OFF
GO
SET IDENTITY_INSERT [dbo].[PositionRole] ON 
GO
INSERT [dbo].[PositionRole] ([Id], [PositionId], [RoleId], [DateTime]) VALUES (1, 1, 1, CAST(N'2019-02-26T18:14:57.717' AS DateTime))
GO
INSERT [dbo].[PositionRole] ([Id], [PositionId], [RoleId], [DateTime]) VALUES (2, 1, 3, CAST(N'2019-02-26T18:14:57.723' AS DateTime))
GO
INSERT [dbo].[PositionRole] ([Id], [PositionId], [RoleId], [DateTime]) VALUES (3, 2, 1, CAST(N'2019-02-26T19:18:03.293' AS DateTime))
GO
INSERT [dbo].[PositionRole] ([Id], [PositionId], [RoleId], [DateTime]) VALUES (4, 2, 24, CAST(N'2019-02-26T19:18:03.300' AS DateTime))
GO
INSERT [dbo].[PositionRole] ([Id], [PositionId], [RoleId], [DateTime]) VALUES (5, 3, 2, CAST(N'2019-02-26T19:19:59.943' AS DateTime))
GO
INSERT [dbo].[PositionRole] ([Id], [PositionId], [RoleId], [DateTime]) VALUES (6, 3, 27, CAST(N'2019-02-26T19:19:59.960' AS DateTime))
GO
INSERT [dbo].[PositionRole] ([Id], [PositionId], [RoleId], [DateTime]) VALUES (7, 3, 22, CAST(N'2019-02-26T19:19:59.967' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[PositionRole] OFF
GO
SET IDENTITY_INSERT [dbo].[Project] ON 
GO
INSERT [dbo].[Project] ([Id], [Name], [OrganizationId]) VALUES (1, N'Smart product 1', 1)
GO
INSERT [dbo].[Project] ([Id], [Name], [OrganizationId]) VALUES (2, N'Usual product 1', 1)
GO
SET IDENTITY_INSERT [dbo].[Project] OFF
GO
SET IDENTITY_INSERT [dbo].[Team] ON 
GO
INSERT [dbo].[Team] ([Id], [Name], [ProjectId], [OrganizationId]) VALUES (1, N'Smart team 1', 1, 1)
GO
INSERT [dbo].[Team] ([Id], [Name], [ProjectId], [OrganizationId]) VALUES (2, N'Smart team 2', 1, 1)
GO
INSERT [dbo].[Team] ([Id], [Name], [ProjectId], [OrganizationId]) VALUES (3, N'Usual team 1', 2, 1)
GO
SET IDENTITY_INSERT [dbo].[Team] OFF
GO
/****** Object:  Index [IX_Role]    Script Date: 27.02.2019 11:07:37 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Role] ON [dbo].[EcfRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Position] ADD  CONSTRAINT [DF_Position_CreatedDate]  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Position] ADD  CONSTRAINT [DF_Position_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[PositionRole] ADD  CONSTRAINT [DF_PositionRole_DateTime]  DEFAULT (getutcdate()) FOR [DateTime]
GO
ALTER TABLE [dbo].[EcfCompetenceLevel]  WITH CHECK ADD  CONSTRAINT [FK_CompetenceLevel_Competence] FOREIGN KEY([CompetenceId])
REFERENCES [dbo].[EcfCompetence] ([Id])
GO
ALTER TABLE [dbo].[EcfCompetenceLevel] CHECK CONSTRAINT [FK_CompetenceLevel_Competence]
GO
ALTER TABLE [dbo].[EcfEvaluation]  WITH CHECK ADD  CONSTRAINT [FK_EcfEvaluation_EmployeeEvaluation] FOREIGN KEY([EvaluationId])
REFERENCES [dbo].[EmployeeEvaluation] ([Id])
GO
ALTER TABLE [dbo].[EcfEvaluation] CHECK CONSTRAINT [FK_EcfEvaluation_EmployeeEvaluation]
GO
ALTER TABLE [dbo].[EcfRoleCompetence]  WITH CHECK ADD  CONSTRAINT [FK_EcfRoleCompetence_EcfCompetence] FOREIGN KEY([CompetenceId])
REFERENCES [dbo].[EcfCompetence] ([Id])
GO
ALTER TABLE [dbo].[EcfRoleCompetence] CHECK CONSTRAINT [FK_EcfRoleCompetence_EcfCompetence]
GO
ALTER TABLE [dbo].[EcfRoleCompetence]  WITH CHECK ADD  CONSTRAINT [FK_EcfRoleCompetence_EcfRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[EcfRole] ([RoleId])
GO
ALTER TABLE [dbo].[EcfRoleCompetence] CHECK CONSTRAINT [FK_EcfRoleCompetence_EcfRole]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_EmployeeType] FOREIGN KEY([EmployeeTypeId])
REFERENCES [dbo].[EmployeeType] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_EmployeeType]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Organization] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Organization]
GO
ALTER TABLE [dbo].[EmployeePosition]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePosition_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeePosition] CHECK CONSTRAINT [FK_EmployeePosition_Employee]
GO
ALTER TABLE [dbo].[EmployeePosition]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePosition_EmployeeRelations] FOREIGN KEY([RelationId])
REFERENCES [dbo].[EmployeeRelations] ([Id])
GO
ALTER TABLE [dbo].[EmployeePosition] CHECK CONSTRAINT [FK_EmployeePosition_EmployeeRelations]
GO
ALTER TABLE [dbo].[EmployeePosition]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePosition_Organization] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[EmployeePosition] CHECK CONSTRAINT [FK_EmployeePosition_Organization]
GO
ALTER TABLE [dbo].[EmployeePosition]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePosition_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([Id])
GO
ALTER TABLE [dbo].[EmployeePosition] CHECK CONSTRAINT [FK_EmployeePosition_Position]
GO
ALTER TABLE [dbo].[EmployeeRelations]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRelations_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeRelations] CHECK CONSTRAINT [FK_EmployeeRelations_Employee]
GO
ALTER TABLE [dbo].[EmployeeRelations]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRelations_Manager] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeRelations] CHECK CONSTRAINT [FK_EmployeeRelations_Manager]
GO
ALTER TABLE [dbo].[EmployeeRelations]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRelations_Organization] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[EmployeeRelations] CHECK CONSTRAINT [FK_EmployeeRelations_Organization]
GO
ALTER TABLE [dbo].[EmployeeRelations]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRelations_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[EmployeeRelations] CHECK CONSTRAINT [FK_EmployeeRelations_Project]
GO
ALTER TABLE [dbo].[EmployeeRelations]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRelations_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([Id])
GO
ALTER TABLE [dbo].[EmployeeRelations] CHECK CONSTRAINT [FK_EmployeeRelations_Team]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_EmployeeCreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_EmployeeCreatedBy]
GO
ALTER TABLE [dbo].[Position]  WITH CHECK ADD  CONSTRAINT [FK_Position_EmployeeUpdatedBy] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[Position] CHECK CONSTRAINT [FK_Position_EmployeeUpdatedBy]
GO
ALTER TABLE [dbo].[PositionRole]  WITH CHECK ADD  CONSTRAINT [FK_PositionRole_EcfRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[EcfRole] ([Id])
GO
ALTER TABLE [dbo].[PositionRole] CHECK CONSTRAINT [FK_PositionRole_EcfRole]
GO
ALTER TABLE [dbo].[PositionRole]  WITH CHECK ADD  CONSTRAINT [FK_PositionRole_Position] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Position] ([Id])
GO
ALTER TABLE [dbo].[PositionRole] CHECK CONSTRAINT [FK_PositionRole_Position]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_Organization] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_Organization]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_Project]
GO
USE [master]
GO
ALTER DATABASE [TechnicalEvaluation] SET  READ_WRITE 
GO
