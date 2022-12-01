export default {
    clearMocks: true,
    coverageDirectory: "coverage",
    coverageProvider: "babel",
    preset: "ts-jest",
    testEnvironment: "node",
    testRegex: [".*\\.(test|spec)?\\.(ts|tsx)$"],
    passWithNoTests: false,
    globals: {
      "ts-jest": {
        isolatedModules: true,
      },
    },
    coverageThreshold: {
      global: {
        branches: 95,
        functions: 95,
        lines: 95,
        statements: 95,
      },
    },
  }
  