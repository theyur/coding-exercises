class Calculator {
    private readonly _number: number;
    private _divisionError: boolean;

    constructor(value: number, divisionError: boolean = false) {
        this._number = value;
        this._divisionError = divisionError;
    }

    add(value: number): Calculator {
        if (this._divisionError) return new Calculator(this._number, true);

        return new Calculator(this._number + value);
    }

    subtract(value: number): Calculator {
        if (this._divisionError) return new Calculator(this._number, true);

        return new Calculator(this._number - value);
    }

    multiply(value: number): Calculator {
        if (this._divisionError) return new Calculator(this._number, true);

        return new Calculator(this._number * value);
    }

    divide(value: number): Calculator {
        if (this._divisionError || value === 0) return new Calculator(this._number, true);

        return new Calculator(this._number / value);
    }

    power(value: number): Calculator {
        if (this._divisionError) return new Calculator(this._number, true);

        return new Calculator(Math.pow(this._number, value));
    }

    getResult(): number | string {
        return this._divisionError ? "Division by zero is not allowed" : this._number;
    }
}