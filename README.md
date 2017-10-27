**[Table of Contents](http://tableofcontent.eu)**
- [Frends.Community.WebForm](#frendscommunitywebsubmitform)
  - [Contributing](#contributing)
  - [Documentation](#documentation)
    - [Parameters](#parameters)
    - [Options](#options)
    - [Result](#result)
  - [License](#license)


# Frends.Community.WebForm
FRENDS Task that posts web form.

## Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

## Documentation

### Parameters

| Property            |  Type               | Description								         | Example                     |
|---------------------|---------------------|----------------------------------------------------|-----------------------------|
| FormParameters	  | Array(string,string)| List of form inputs the forum should contain       | `input` `message` |
| FormFileParameters  | Array(string,string)| List of form file inputs the forum should contain  | `input` `c:\temp\foo.txt` |

### Options

| Property                                    | Type           | Description                                    | Example                   |
|---------------------------------------------|----------------|------------------------------------------------|---------------------------|
| Address									  | string         | Address of the web form submit | |
| UserName                                    | string         | | |
| Password                                    | string         | | |

### Result

| Property        | Type     | Description                      |
|-----------------|----------|----------------------------------|
| FilePath        | HttpResponseMessage   | webrequest result|



## License

This project is licensed under the MIT License - see the LICENSE file for details
