import * as _ from 'lodash';

export class Utils {

    static generateArray(n: number): Array<number> {
        return  _.times(n);
    }
}