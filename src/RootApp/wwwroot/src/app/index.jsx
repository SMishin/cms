var React = require('react');
var ReactDOM = require('react-dom');
var Router = require('react-router').Router;
var browserHistory   =  require('react-router').browserHistory ;
var Route = require('react-router').Route;
var App = require('./App');
var Repos = require('./Repos');
var About = require('./About');

ReactDOM.render(
    <Router history={browserHistory }>
        <Route path="/_cms" component={App}>
            <Route path="/_cms/repos" component={Repos}/>
            <Route path="/_cms/about" component={About}/>
        </Route>

    </Router>,
    document.getElementById('app')
);