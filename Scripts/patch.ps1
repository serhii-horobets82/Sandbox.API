Write-Host "Start patching file Models/Employee.cs"
(Get-Content -path "Models/Employee.cs" -Raw) -replace '}.\n}',"    [ForeignKey(""UserId"")]`r`n        [InverseProperty(""Employee"")]`r`n        public virtual Evoflare.API.Auth.Models.ApplicationUser Users { get; set; }`n }`n}" | Out-File "Models/Employee.cs" -Encoding UTF8

Write-Host "Start patching file Data/EvoflareDbContext.cs"
(Get-Content -path "Data/EvoflareDbContext.cs" -Raw) -replace '}\);\r\n        ',"});`n`r            base.OnModelCreating(modelBuilder);`r`n        " -replace ": DbContext",": BaseDbContext" -replace "#warning.*","" | Out-File "Data/EvoflareDbContext.cs" -Encoding UTF8