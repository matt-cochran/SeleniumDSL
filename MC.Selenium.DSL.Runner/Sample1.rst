test the home page. 
show that the page loads.
use ie, firefox.
define target page as http://www.google.com.
define search box as element named 'q'.
{
    go to the target page.
    clear the search box.
    send 'asdf' to the search box.
    assert the search box has value 'asdf'
}