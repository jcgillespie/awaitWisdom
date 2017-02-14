Title: A New Stack 
Created: 2016-03-19T14:24:36.5369562-05:00
Published: 2016-03-19T15:00:00.0000000-05:00
Tags: 
 - Blogging
 - Tech
 - Meta
---
#### Teaching an old blog a new stack
After messing around with the Ghost blog engine and Node.js, I knew I wanted a new technology stack. My daily work is typically in .NET, so that was preferrable. Playing with and learning Node.js was fine, but I found that having the learning curve between me and writing kept me from writing.

I was intrigued by the concept of a static website. I looked at Jekyl, and Octopress, which introduced me to the static workflow and GitHub Pages hosting. In the end, their Windows environment support wasn't great and it I still preferred to find something in the .NET framework.

So my list of requirements was pretty small.
* .NET framework based
* Static website
* Supports blogging in [Markdown]
* Easy to learn/use
* Fun to play with

Eventually, I found [Wyam]. Wyam is capable of a lot more than just blogging, but it checked all the right boxes.  Setup was pretty straightforward, and working locally is simple. 

I commit posts to my [GitHub repo][GitHub], [AppVeyor] sees that commit, pulls down the latest release of Wyam, generates the static files, and commits the results to the `gh-pages` branch, which [GitHub Pages][GitHubPages] hosts for me (free!)

Still very much a work in progress, but I'm having fun blogging again. See a typo? Send me a pull request. Feel free to suggest features or submit bugs too.  All the code is licensed under a [MIT License][MIT] and content is available as a [Creative Commons attribution license][CCbyAttrib].

[Wyam]: http://wyam.io/
[Markdown]: http://daringfireball.net/projects/markdown/
[GitHub]: https://github.com/jcgillespie/awaitWisdom
[AppVeyor]: http://www.appveyor.com/
[GitHubPages]:https://pages.github.com/
[MIT]:https://opensource.org/licenses/MIT
[CCbyAttrib]:http://creativecommons.org/licenses/by/4.0/deed.en_US