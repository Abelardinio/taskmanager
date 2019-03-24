import { PagingInfo } from './PagingInfo';
import { BaseFilter } from './BaseFilter';

export class ProjectFilter extends BaseFilter {
  constructor(
    public PagingInfo: PagingInfo,
    public Value?: string
  ) {
    super(PagingInfo);
  }
}
