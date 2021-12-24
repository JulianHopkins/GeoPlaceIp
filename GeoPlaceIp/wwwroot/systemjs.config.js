System.config({
    packages: {
        'app': {
            defaultExtension: 'js',
            main: "./lib/ts/GetItems"
        },
    transpiler: 'typescript',
    typescriptOptions: { emitDecoratorMetadata: true },
    }
});