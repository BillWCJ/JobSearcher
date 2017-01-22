UW Coop Job Finder: a faster and better way to find Coop jobs on JobMine  
NOTE: As of January 2017, JobMine has been deprecated by the new WaterlooWorks. This app is no longer functional due to the change of data source and data model. Future support for WaterlooWorks is not planned.

- FAST: look for postings, activate multiple filters, search for key words, and get additional data all within seconds; No more waiting for JobMine to respond and opening hundreds of Chrome tabs
- Offline job search and 24hr support: you can now look for jobs even when JobMine is shut down after midnight
- Embedded web browsers: web browsers that automatically search for the employer you are looking at: Google search for the employer name and Google Map search the employer location
- Job review data: retrieve and display job rating data from RateMyCoopJob.com
- Add to JobMine shortlist: one click to shortlist the posting on JobMine and apply for the position
- Export data: export the posting data into human read-able text format to search in Word; or JSON to consume into other programs
- Job preview (tooltip): take a quick peek at the job description with a tooltip that pop up when hovering over any job posting (No more opening job posting in a new tab, go to the new tab, scroll to read job description and find out you don’t like it, closing the tab, then going back to the job search table non-sense).
- Customizable layout with multimonitor support: customizable docking style UI windows/controls that is similar many IDEs. Drag and drop different windows to multiple monitors (Imagine reading job search table in one monitor and the job description automatically show up on another. After you finishes reading, maybe checkout job rating, Google search, and Maps search of the employer on a third monitor. Isn’t that the dream?)
- Complex Filtering: create unlimited filter objects that have the following abilities
  - Keyword Filter: Although JobMine only allows key word searches in “Job Title” and “Employer Title”, but now you can search in all the job posting information. (E.g. Search for the skill “C#” in “Job Description”)
  - Filter Stacking: activate and deactivate filters to see jobs with different combinations of characteristics (E.g. Activate “C#” and “Hometown” filter to only see C# develop positions in your hometown. Then deactivate “Hometown” filter to see C# jobs everywhere)
  - Anti-filters: focus and set-up criteria to filter out job postings that will not interest you (E.g. Filter out jobs where “Job Title” contains “QA”; filter out jobs where “Location” contains “Ottawa”)
  - Point filters: Aggregate scores for postings that satisfy a criterion (E.g. Add 10 points if “Job Description” contains “C#”; -10 points if “Job Title” contains “Junior”)
  - Others features including: unlimited selection for “Discipline” Filter; automatic categorization of coop durations (such as 4-month work term, 8-month work term, both); number of Openings (find jobs that will have other coops in the company), number of Applicants, levels…
  - AND & OR logic map (planned feature): drag and drop the filter objects into a graphical logic expression graph to find jobs that satisfy a complex set of requirements
  
  
Feature Details: https://github.com/BillWenChaoJiang/JobSearchEnhancer/wiki/Program-and-Feature-Breakdown
  
  
Setup Instructions: <https://github.com/BillWenChaoJiang/JobSearchEnhancer/wiki/How-to-Setup>  
  
  
  
Main Enabling Techologies:
- C#/.Net
- MS SQL Server (ORM: Entity Framework)
- WPF (MVVM: Prism, WPF Extended Toolkit: Avalon Dock)
- HTMLAgilityPack (Scraping websites and traversing HTML DOM)
