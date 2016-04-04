# InventorySystem
Web API to manage items in the inventory.

# The project structure
The solution includes the service tier where all the business logic is, the web API, the unit tests project and the web client project (empty now). 

# Validation
The solution includes the service tier where all the business logic is, the web API, the unit tests project and the web client project (empty now). 

# Schduling
Quartz.net is used to schedule expiration items. Ideally, it should be in a separate project with service bus messaging, but for simplicity sake it is in the same project.

# Data persistance
Repository is in memory

# Notification
A fake simple email service that would send an email for the deleted label. At the moment doesn't send anything anywhere, just for demonstration purposes.

# The client
to be implemented

# Unit testing
Just one for demonstration purpose

# How to run the project
The projects require .Net 4.6.1 to be installed. The project can be run in the debug mode in iis express, messages can be submitted using fiddler. 