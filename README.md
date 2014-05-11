SendToYoutube
=============

Utility to send youtube links to XBMC from command line, linux or windows


Usage:
------

   $ SendToYoutube <media center url> <youtube url>
   $ SendToYoutube <media center id> <youtube url>


Examples:
---------

   $ SendToYoutube http://userid:password123@xbmc-hostname:8080 https://www.youtube.com/watch?v=mwEGHlqbjw8
   $ SendToYoutube myxbmc https://www.youtube.com/watch?v=mwEGHlqbjw8

In order to use media center id instead or URL, register myxbmc in SendToYoutube.config:

<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <appSettings>
        <add key="mc:myxbmc" value="http://userid:password123@xbmc-hostname:port"/>
    </appSettings>
</configuration>
