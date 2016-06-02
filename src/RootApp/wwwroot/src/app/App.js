var React = require('react');

var template =  require('./App-template');
module.exports = React.createClass({
    render: function() {
        return template.apply(this, []);
    }
});