![](http://i.imgur.com/ZbNpyDX.png) EasyImgur
=========

EasyImgur is a small and simple tool to easily upload images to imgur.com from your desktop.


Using EasyImgur
---

- [Download EasyImgur](https://github.com/Rycul/EasyImgur/releases)(zip file), extract to a folder of your choosing (For example: C:\Program Files\EasyImgur\).

- Run EasyImgur.exe, the EasyImgur icon will appear in your system tray (if this is your first time running EasyImgur, EI will save its location and start automatically next time you start your computer):

![](http://i.imgur.com/3UL7hBe.png)

- Open the EasyImgur menu by right-clicking on the tray icon:
![](http://i.imgur.com/vmGPAlO.png)

- Upload a file or an image on your clipboard (for example a screenshot or a text URL that you copied) anonymously (*not* to an Imgur account) by using the *Upload from file...* and *Upload from clipboard* options.

- To upload images to an Imgur account, you must first authorize EasyImgur to use your account. To do so, open the settings dialog by selecting *Settings* from the right-click menu. This opens an EasyImgur window. Go to the *Account* tab:
![](http://i.imgur.com/i34VILO.png)

- Select *Authorize this app...*. This will open the Imgur authorization page in your browser. If you are already logged in, you will see this message:
- ![](http://i.imgur.com/zdcWcp0.png)

- Select *Allow* and copy the code that is displayed on the next page.

- Go back to the EasyImgur window. A popup has been opened for you to enter the code that was just given to you:
![](http://i.imgur.com/wdxlEPC.png)

- Select *OK* and wait while the application talks to Imgur to receive authorization codes. Once the authorization is successful, a balloon popup will appear from the system tray to tell you this, and the EasyImgur window will display *Status: Authorized*:
![](http://i.imgur.com/U11116k.png)

NOTE: To revoke access, you can visit http://imgur.com/account/settings/apps, where you can manage all apps that have access to your account. Note that the *Forget Tokens* button in EasyImgur only makes EasyImgur discard the current codes needed to access your account. If these codes have somehow been captured by malicious software or persons, they might still be able use them to access your account unless you revoke access completely!

- You can now use the *Upload from file... (to account)* and *Upload from clipboard (to account)* options from the EasyImgur menu.

NOTE: EasyImgur will periodically refresh its authorization codes. When this happens, a balloon popup will appear to inform you that EasyImgur has successfully done so (or not, if that should be the case).

**Uploaded images (and their URLs) can be found under the** ***History*** **tab. From here you can select images, see information about the selected image including a thumbnail preview of the selected image, its Imgur ID, its Imgur URL, its deletehash, and whether it was uploaded anonymously or to an account. Also, there is a convenient button here that allows you to remove an uploaded image from Imgur.**

Settings
---

EasyImgur has a number of different customizable settings. These can be accessed from the *General* tab in the EasyImgur settings window:

![](http://i.imgur.com/xiQLbFD.png)

- **Clear clipboard on upload**: This option determines whether the clipboard will be cleared after uploading from it.
- **Automatically copy links to clipboard**: Determines whether links to images that have succesfully been uploaded are automatically placed on your clipboard (ready to be pasted somewhere with Ctrl+V).
- **Preferred image format**: This option gives a hint to Imgur as to what image format is preferred. Note that it only hints at this by providing the source image in a certain format. Imgur is can (and will) change the format to something else if it chooses to do so.
- **Use this title format** and **Use this description format**: These define two strings that are used as the title and description for every uploaded image. The strings can contain special symbols which are converted to set values before uploading. For a complete set of symbols, click the *Format?* button. An example string containing special symbols and its final form is: *Image_%n%_%date%_%time%*, might turn out to be *Image_0_19-08-2013_13:37:00* because *%n%* is converted to an integer denoting how many images have currently been uploaded, *%date* is converted to the current date in DD-MM-YYYY format, and *%time%* is converted to the current time in HH-MM-SS format.
- **Launch EasyImgur at Windows start**: This determines whether EasyImgur should have a registry key set to allow it to be started when Windows is started. Note that if you move the EasyImgur.exe file, the registry path will become incorrect and a manual start of EasyImgur is required to restore it (EasyImgur detects this and can take care of that when it starts).
