# flexigin-net

flexigin-net is the .NET implementation of a [flexigin](https://github.com/robbz/flexigin)-server.

If you have any questions or feedback, feel free to contact me using [@robertobez](https://twitter.com/robertobez) on Twitter.

## Installation

Add the `flexigin.core.dll` reference to the desired web project.

## Quick start

Using flexigin-net for the server-side is very simple: Just reference one http-handler in the `web.config`, which will do the rest.
For example if your entry point should be *http://example.com/flexigin/* the configuration will look like this:

    <system.web>
        <httpHandlers>
            <add verb="GET" path="flexigin/" type="Flexigin.Core.Handler.FlexiginHandler"/>
        </httpHandlers>
    </system.web>

    <system.webServer>
        <handlers>
            <add name="flexigin" verb="GET" path="flexigin/" type="Flexigin.Core.Handler.FlexiginHandler"/>
        </handlers>
    </system.webServer>

The handler will look for a querystring parameter `p`, which means the path of the component desired.

You won't pass everytime the full path, just specify a `basePath` for flexigin:

    <add key="flexigin:basePath" value="/components/"/>

To cache the requests (browser-side), there is a propriate option:

    <add key="flexigin:cacheMinutes" value="60"/>

As in this example we use a HttpHandler, which is meant to have only one access point, we use the querystring for the path and the route for the client will change to:

    fx.set('route', '/flexigin/?p={component}/{type}/');

### Example

If the client requests `user/js`, this means to return the javascript for the user component.

As the `basePath` is `/components/`, flexigin will look for `/components/user/*.js`, concatenate and minify them, to return only one result at the end.

## Running the tests

The Tests are located in the `Flexigin.Test` project and are build with NUnit. You can run them with a test-runner as for example resharper provides.

## License

The MIT License (MIT)
Copyright (c) 2013 Roberto Bez.
 
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.