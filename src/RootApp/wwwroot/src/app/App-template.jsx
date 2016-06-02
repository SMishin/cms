var React = require('react');
var Link = require('react-router').Link;
module.exports = function () {
    return (
        <div class="app">
            <div>
                <h1>React Router Tutorial</h1>
                <ul role="nav">
                    <li><Link to="/_cms/about">Abousst</Link></li>
                    <li><Link to="/_cms/repos">Repos</Link></li>
                </ul>
                {/* add this */}
                {this.props.children}
            </div>
        </div>

    );
};
