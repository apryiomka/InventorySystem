# InventorySystem
Web API to manage items in the inventory.

# Authentication and Authorization
No Authentication was implemented. I can add simple Authentication or other forms of authentication if need, for example security token authentication (with OWIN) can also be implemented.

# The project structure
The solution includes the service tier where all the business logic is, the web API, the unit tests project and the web client project (empty now). I will also add the UI project later.

# Validation
Simple validaation was implemented for the Label only (required field). More validation can be added like expiration date in future, etc. I did not assume label to be unique. The uniqueness of the item is determined by Guid, not label, meaning multiple items with the same label can be created. 
This behaivior can be changed if needed. If multiple items with the same label exist, all of them will be deleted.

# Schduling
Quartz.net is used to schedule expiration items. Ideally, it should be in a separate project with service bus messaging, but for simplicity sake it is in the same project.

# Data persistance
Repository is in memory a simple dictionary with Guid key

# Notification
A fake simple email service that would "send" an email for the deleted label items or items deleted by scheduled job. At the moment the service doesn't send anything anywhere and just for the demonstration purpose. I can add SendGrid relay to send emails if needed.

# The client
to be implemented

# Unit testing
Just one for demonstration purpose. I need to write more tests for the Controllers, and the inventory manager.

# How to run the project
The projects require .Net 4.6.1 to be installed. The project can be run in the debug mode in iis express, messages can be submitted using fiddler. 

# Remaining items
I still need to write more unit tests as well as complete the Client and the UI piece.