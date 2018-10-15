import * as _ from 'lodash';

export class Utils {

    public static generateArray(n: number): Array<number> {
        return  _.times(n);
    }
}
