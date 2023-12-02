using System.Linq.Expressions;

namespace ChatTeamChallenge.Persistence.Specifications;

/// <summary>
/// Represents the abstract base class for specifications.
/// </summary>
/// <typeparam name="TEntity">The class type.</typeparam>
public abstract class Specification<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Converts the specification to an expression predicate.
    /// </summary>
    /// <returns>The expression predicate.</returns>
    protected abstract Expression<Func<TEntity, bool>> ToExpression();

    public static implicit operator Expression<Func<TEntity, bool>>(Specification<TEntity> specification) =>
        specification.ToExpression();
}