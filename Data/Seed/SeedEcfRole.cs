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
    
		public static bool SeedEcfRole(EvoflareDbContext context)
        {
            if (context.EcfRole.Any()) return false;
            IDbContextTransaction trans = null;
            
			if(true && context.Database.IsSqlServer())
			{
			    trans = context.Database.BeginTransaction();
                context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfRole] ON");
			}
            var items = new[]
            {
				new EcfRole {Id = 1, RoleId = 6, Name = @"Developer", Summary = @"Designs and/or codes components to meet solution specifications", Description = @"Ensures building and implementing of ICT applications. Contributes to lowlevel design. Writes code to ensure optimum efficiency and functionality and user experience." },
				new EcfRole {Id = 2, RoleId = 22, Name = @"Technical Specialist", Summary = @"Maintains and repairs hardware, software and service applications", Description = @"To effectively maintain customer hardware/software. Responsible for delivering timely and effective repairs to ensure optimal system performance and superior customer satisfaction." },
				new EcfRole {Id = 3, RoleId = 24, Name = @"Solution Designer", Summary = @"Provides the translation of business requirements into end-to-end IT solutions", Description = @"Proposes and designs solutions in line with technical architecture which fit business requirements and support change." },
				new EcfRole {Id = 4, RoleId = 9, Name = @"Digital Consultant", Summary = @"Supports understanding of how digital technologies add value to a business", Description = @"Maintains a technology watch to inform stakeholders of existing and emerging technologies and their potential to add business value. Supports the identification of needs and solutions for achieving business and IS strategic goals." },
				new EcfRole {Id = 5, RoleId = 8, Name = @"Enterprise Architect", Summary = @"Designs and maintains the holistic architecture of business processes and Information Systems.", Description = @"Maintains a holistic perspective of the organisation strategy, processes, information, security and ICT assets. Links the mission, strategy and business processes to the IT strategy. Ensures project choices are integrated consistently, efficiently and in a sustainable manner according to the enterprise’s digital standards" },
				new EcfRole {Id = 6, RoleId = 21, Name = @"Systems Architect", Summary = @"Plans, designs and integrates ICT system components including hardware, software and services", Description = @"Designs, integrates and implements complex technical ICT solutions ensuring procedures and models for development are current and comply with common standards. Monitors new technology developments and applies if appropriate. Provides technological design leadership." },
				new EcfRole {Id = 7, RoleId = 13, Name = @"Digital Educator", Summary = @"Educates and trains Professionals to reach optimal digital competence to support business performance.", Description = @"Provide the knowledge and skills required to ensure that people are able to effectively perform tasks in the workplace." },
				new EcfRole {Id = 8, RoleId = 20, Name = @"Systems Analyst", Summary = @"Analyses organisation requirements and specifies software and system requirements for new IT solutions", Description = @"Ensures the technical design and contributes to the implementation of new and/or enhanced software provision. Provides solutions for the improvement of organisational efficiency and productivity." },
				new EcfRole {Id = 9, RoleId = 2, Name = @"Business Analyst", Summary = @"Analyses the business domain and optimises business performance through technology application", Description = @"Analyses the information and the processes needed to support business plans. Formulates functional and nonfunctional requirements of the business organisation and advises on the lifecycle of the information solutions. Evaluates the impact in terms of change management." },
				new EcfRole {Id = 10, RoleId = 26, Name = @"Devops Expect", Summary = @"Implements processes and tools to successfully deploy DevOps techniques across the entire solution development lifecycle.", Description = @"To apply a cross-functional, collaborative approach for the creation of customer-centric software solutions. Introduce automation throughout the software production system to deliver better software faster" },
				new EcfRole {Id = 11, RoleId = 29, Name = @"Scrum Master", Summary = @"Leads and coaches an agile team", Description = @"Creates a high performance self-managed dynamic team minimising impediments to development progress. Drives team by applying the agile process to achieve an optimesed work-flow through continuous improvement. Supports team goals and coordinates activities with other teams" },
				new EcfRole {Id = 12, RoleId = 15, Name = @"Project Manager", Summary = @"Manages projects to achieve optimal performance and results.", Description = @"Defines, implements and manages projects from conception to final delivery. Responsible for achieving optimal results, conforming to standards for quality, safety and sustainability and complying with defined scope, performance, costs, and schedule. Deploys agile practices where applicable." },
				new EcfRole {Id = 13, RoleId = 1, Name = @"Account Manager", Summary = @"Senior focal point for client sales and customer satisfaction.", Description = @"Builds business relationships with clients to facilitate the sale of hardware, software, telecommunications or ICT services. Identifies opportunities and manages sourcing and delivery of products to customers. Has responsibility for achieving sales targets and maintaining profitability." },
				new EcfRole {Id = 14, RoleId = 3, Name = @"Business Information Manager", Summary = @"Proposes, plans and manages functional development of the Information System (IS) focusing upon the needs of users.", Description = @"Aligns the Information System to the business strategy within their area/domain. Ensures continuous enhancement whilst accounting for user requirements, service quality and budgetary constraints." },
				new EcfRole {Id = 15, RoleId = 4, Name = @"Chief Information Officer", Summary = @"Develops and maintains Information Systems to generate value for the business and meet the organisation’s needs.", Description = @"Ensures the alignment of the Information Systems strategy with the business strategy. Provides leadership for the implementation and development of the organisations architecture and applications." },
				new EcfRole {Id = 16, RoleId = 5, Name = @"Database Administrator", Summary = @"Designs, implements, or monitors and maintains data sets, structured (databases) and unstructured (big data).", Description = @"Administer and monitor data management systems and ensures design, consistency, quality and security." },
				new EcfRole {Id = 17, RoleId = 7, Name = @"Digital Media Specialist", Summary = @"Integrates digital technology components for internal and external communication purposes.", Description = @"Designs and codes social media applications and websites. Makes recommendations on Application Programming Interface (API) and supports efficiency through appropriate content management systems." },
				new EcfRole {Id = 18, RoleId = 10, Name = @"ICT Operations Manager", Summary = @"Manages operations, people and overall ICT resources.", Description = @"Implements and maintains a designated part of an ICT operation ensuring that activities are conducted in accordance with organisational rules, processes and standards. Plans changes and implements them in accordance with organisational strategy and budget. Risk manages and ensures the effectiveness of the ICT infrastructure." },
				new EcfRole {Id = 20, RoleId = 11, Name = @"Information Security Manager", Summary = @"Leads and manages the organisation information security policy.", Description = @"Defines the information security strategy and manages implementation across the organisation. Embeds proactive information security protection by assessing, informing, alerting and educating the entire organisation." },
				new EcfRole {Id = 21, RoleId = 12, Name = @"Information Security Specialist", Summary = @"Ensures the implementation of the organisation’s information security policy by the secure and appropriate use of ICT resources.", Description = @"Defines, proposes and implements necessary information security technique and practices in compliance with information security standards and procedures. Contributes to security practices, awareness and compliance by providing advice, support, information and training." },
				new EcfRole {Id = 22, RoleId = 14, Name = @"Network Specialist", Summary = @"Ensures the alignment of the network, including telecommunication and/or computer infrastructure to meet the organisation’s communication needs.", Description = @"Manages and operates a networked information system, solving problems and faults to ensure defined service levels. Monitors and improves network performances and security." },
				new EcfRole {Id = 23, RoleId = 16, Name = @"Quality Assurance Manager", Summary = @"Ensures that processes and organisations implementing Information Systems comply to quality policies.", Description = @"Establishes and operates an ICT quality approach aligned with the organisation’s culture. Commits the organisation to the achievement of quality goals and an encourages an environment of continuous improvement." },
				new EcfRole {Id = 24, RoleId = 17, Name = @"Service Support", Summary = @"Provides remote or onsite diagnosis or guidance to internal or external clients with technical issues.", Description = @"To provide user support and troubleshoot ICT problems and issues. The primary objective is to enable users to maximize their productivity through efficient and secure use of ICT equipment or software applications." },
				new EcfRole {Id = 25, RoleId = 18, Name = @"Service Manager", Summary = @"Plans, implements and manages solution provision.", Description = @"Manages the definition of Service Level Agreements (SLAs), Operational Level Agreements (OLAs) contracts and Key Performance Indicators (KPIs). Provides people management of staff monitoring, reporting and fulfilling service activities. Takes mitigation action in case of nonfulfilment of agreements." },
				new EcfRole {Id = 26, RoleId = 19, Name = @"Systems Administrator", Summary = @"Administers ICT System components to meet service requirements.", Description = @"Installs software, configures and upgrades ICT systems. Administers day-to-day operations to satisfy continuity of service, recovery, security and performance needs." },
				new EcfRole {Id = 27, RoleId = 23, Name = @"Test Specialist", Summary = @"Designs and performs testing plans.", Description = @"Ensures delivered or existing products, applications or services comply with technical and user needs and specifications. For existing systems, applications, innovations and changes; diagnoses failure of products or services to meet specification." },
				new EcfRole {Id = 28, RoleId = 25, Name = @"Digital Transformation", Summary = @"Provides leadership for the implementation of the digital transformation strategy of the organisation.", Description = @"Drive cultural change and build digital capability to deliver innovative business models and processes." },
				new EcfRole {Id = 29, RoleId = 27, Name = @"Data Scientist", Summary = @"Leads the process of applying data analytics. Delivers insights from data by optimising the analytics process and presenting visual data representations.", Description = @"Finds, manages and merges multiple data sources and ensures consistency of datasets. Identifies the mathematical models, selects and optimises the algorhythms to deliver business value through insights. Communicates patterns and recommends ways of applying data." },
				new EcfRole {Id = 30, RoleId = 28, Name = @"Data Specialist", Summary = @"Ensures the implementation of the organisations data management policy.", Description = @"Ensures asset protection through the provision of clean, consistent, quality assured data. Maintains the integrity of data, stores and searches data and supports presentation of data analysis." },
				new EcfRole {Id = 31, RoleId = 30, Name = @"Product Owner", Summary = @"Represents the voice of the customer in an Agile team.", Description = @"Understands customer requirements and validates that the developed software solution meets requirements. Links business and Agile teams." },

            };
            context.EcfRole.AddRange(items);

            context.SaveChanges();

			if(true && context.Database.IsSqlServer())
            {
				context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [EcfRole] OFF");
				trans.Commit();
			}
            
            return true;
        }
    }
}
