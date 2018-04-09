Please follow all of these these steps:
asdasd
To install the service:
1. Open VS command line as ADMINISTRATOR
2. Edit AutoDevSync.reg to your working path and backup path
3. Type installutil AutoDevSync.exe from the location the EXE is located
4. On the popup: type the user name and password logged on your PC like this:  docusignhq\<username>
5. start the service.
6. To verify it works: If the destination folder don't exist, the service will create it on start. If it already exists you will know only on file change or creation
7. write the code......
8. The changed files are at the backup location
