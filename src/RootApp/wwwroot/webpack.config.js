module.exports = {
    entry: "./src/app",
    devtool: 'inline-source-map',
    output: {
        path: './out',
        filename: "bundle.js"
    },
    resolve: {
        extensions: ['', '.js', '.jsx']
    },
    module: {
        loaders: [
            {
                test: /\.css$/,
                loader: "style!css"
            },
            {
                test: /\.jsx$/,
                loader: "babel"
            }
        ]
    }
};