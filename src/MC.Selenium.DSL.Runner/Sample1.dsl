test the search. 
show that we find selenium.
use firefox, chrome, ie.
define target page as http://www.google.com.
define search_box as element with id 'gbqfq'.
define search_button as element with id 'submit'.
define second_search_box as element as with id "q".
{
	go to www.google.com
    clear the search_box
	send 'selenium' to the search_box
	click the element with id 'gbqfb'
}


